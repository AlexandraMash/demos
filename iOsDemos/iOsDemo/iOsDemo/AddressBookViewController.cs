using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook;
using Foundation;
using UIKit;

namespace iOsDemo
{
	public partial class AddressBookViewController : UIViewController
	{
		protected AddressBookViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NSError error;
			var addressBook = ABAddressBook.Create(out error);

			if (addressBook != null)
			{
				addressBook.RequestAccess(delegate (bool granted, NSError accessError)
				{
					InvokeOnMainThread(() =>
					{
						if (!granted)
						{
							UIAlertView alert = new UIAlertView()
							{
								Title = "Access denied",
								Message = "You won't be able to see your contacts unless you allow access to them"
							};
							alert.AddButton("OK");
							alert.Show();
						}
						else
						{
							ProgressIndicator.Hidden = false;
							ProgressIndicator.StartAnimating();
							AddressBookTable.Hidden = true;
							Task.Run(() =>
							{
								var contacts = addressBook.GetPeople();
								Array.Sort(contacts);
								InvokeOnMainThread(() =>
								{
									AddressBookTable.Source = new ContactsTableSource(contacts, this);
									ProgressIndicator.StopAnimating();
									AddressBookTable.Hidden = false;
									AddressBookTable.ReloadData();
								});
							});
						}
					});
				});
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class ContactsTableSource : UITableViewSource
	{
		AddressBook.ABPerson[] TableItems;

		string[] keys;

		Dictionary<string, List<AddressBook.ABPerson>> indexedTableItems;

		AddressBookViewController owner;

		public ContactsTableSource(AddressBook.ABPerson[] people, AddressBookViewController owner)
		{
			this.owner = owner;
			TableItems = people;
			indexedTableItems = new Dictionary<string, List<AddressBook.ABPerson>>();
			foreach (var contact in people)
			{
				if (indexedTableItems.ContainsKey(contact.LastName[0].ToString()))
				{
					indexedTableItems[contact.LastName[0].ToString()].Add(contact);
				}
				else {
					indexedTableItems.Add(contact.LastName[0].ToString(), new List<AddressBook.ABPerson>() { contact });
				}
			}
			keys = indexedTableItems.Keys.ToArray();
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return keys.Length;
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return indexedTableItems[keys[section]].Count;
		}
		public override string[] SectionIndexTitles(UITableView tableView)
		{
			return keys;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (ContactCell)tableView.DequeueReusableCell(ContactCell.Id, indexPath);
			AddressBook.ABPerson contact = indexedTableItems.ElementAt(indexPath.Section).Value.ElementAt(indexPath.Row);

			cell.NameLabel.Text = $"{contact.LastName} {contact.FirstName}";
			cell.PictureImage.ContentMode = UIViewContentMode.ScaleAspectFit;
			if (contact.HasImage)
			{
				cell.PictureImage.Image = UIImage.LoadFromData(contact.Image);
			}
			else
			{
				cell.PictureImage.Image = UIImage.FromBundle("smiley.png");
			}

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			AddressBook.ABPerson contact = indexedTableItems.ElementAt(indexPath.Section).Value.ElementAt(indexPath.Row);
			var phones = contact.GetPhones();
			var message = "No phone number";
			if (phones != null && phones.Count > 0)
			{
				message = phones.FirstOrDefault().Value;
			}

			UIAlertController okAlertController = UIAlertController.Create(
				$"{contact.LastName} {contact.FirstName}", 
				message, 
				UIAlertControllerStyle.Alert);
			
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			owner.PresentViewController(okAlertController, true, null);

			tableView.DeselectRow(indexPath, true);
		}
	}
}
