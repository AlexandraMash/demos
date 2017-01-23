package com.zeal.integrationsdemo;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.FragmentActivity;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.SignInButton;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.zeal.integrationdemo.R;

public class GooglePlusLoginActivity extends FragmentActivity implements View.OnClickListener, GoogleApiClient.OnConnectionFailedListener //implements GoogleApiClient.OnConnectionFailedListener
{
    private static final int RC_SIGN_IN = 1000;

    private SignInButton mSignInButton;
    private Button mSignOutButton;
    private TextView mNameTextView;
    private TextView mTokenTextView;

    private static GoogleApiClient mGoogleApiClient;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_google_plus);

        mSignInButton = (SignInButton)findViewById(R.id.btn_sign_in);
        mSignInButton.setOnClickListener(this);
        mSignOutButton = (Button)findViewById(R.id.btn_sign_out);
        mSignOutButton.setOnClickListener(this);
        mNameTextView = (TextView)findViewById(R.id.tv_name);
        mTokenTextView = (TextView)findViewById(R.id.tv_token);

        mGoogleApiClient = buildGoogleApiClient();
        mGoogleApiClient.connect();
        mGoogleApiClient.registerConnectionCallbacks(new GoogleApiClient.ConnectionCallbacks() {
            @Override
            public void onConnected(Bundle bundle) {
                mSignInButton.setEnabled(true);
            }

            @Override
            public void onConnectionSuspended(int i) {}
        });
    }

        private GoogleApiClient buildGoogleApiClient() {
        GoogleSignInOptions gso =
                new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
//                        .requestScopes(new Scope(Scopes.PROFILE),
//                                new Scope(Scopes.PLUS_ME),
//                                new Scope(Scopes.EMAIL))
                        .requestEmail()
                        .requestProfile()
                        .build();

        return new GoogleApiClient.Builder(this)
                .enableAutoManage(this, this)
                .addApi(Auth.GOOGLE_SIGN_IN_API, gso).build();
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.btn_sign_in:
                signIn();
                break;
            case R.id.btn_sign_out:
                doSignOut(true);
                break;
        }
    }

    private void signIn() {
        Intent signInIntent = Auth.GoogleSignInApi.getSignInIntent(mGoogleApiClient);
        startActivityForResult(signInIntent, RC_SIGN_IN);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == RC_SIGN_IN) {
            GoogleSignInResult result = Auth.GoogleSignInApi.getSignInResultFromIntent(data);
            handleSignInResult(result);
        }
    }

    private void handleSignInResult(GoogleSignInResult result) {
        if (result.isSuccess()) {
            GoogleSignInAccount acct = result.getSignInAccount();
            if (acct != null) {
                String name = acct.getDisplayName();
                String email = acct.getEmail();
                mNameTextView.setText(getString(R.string.message_signed_in_google_plus, name, email));

                String idTokenString = acct.getServerAuthCode();
                mTokenTextView.setText(idTokenString);

                mSignOutButton.setEnabled(true);
                mSignInButton.setEnabled(false);
            }
        } else {
            mNameTextView.setText(getString(R.string.message_sign_in_failed,
                    result.getStatus()));
            mTokenTextView.setText("");

            mSignOutButton.setEnabled(false);
            mSignInButton.setEnabled(true);
        }
    }

    private void signOut(final boolean withCallback) {
        if(!mGoogleApiClient.isConnected()) {
            mGoogleApiClient.connect();
            mGoogleApiClient.registerConnectionCallbacks(new GoogleApiClient.ConnectionCallbacks() {
                @Override
                public void onConnected(Bundle bundle) {
                    doSignOut(withCallback);
                }

                @Override
                public void onConnectionSuspended(int i) {}
            });
        } else {
            doSignOut(withCallback);
        }
    }

    private void doSignOut(boolean withCallback) {
        PendingResult<Status> statusPendingResult = Auth.GoogleSignInApi.signOut(mGoogleApiClient);
        if(withCallback) {
            statusPendingResult.setResultCallback(mSignOutCallback);
        }
    }

    private ResultCallback<Status> mSignOutCallback = new ResultCallback<Status>() {
        @Override
        public void onResult(@NonNull Status status) {
            if(status.isSuccess()) {
                mNameTextView.setText(R.string.message_not_logged_in);
                mTokenTextView.setText("");

                mSignOutButton.setEnabled(false);
                mSignInButton.setEnabled(true);
            }
        }
    };

    @Override
    protected void onDestroy() {
        clearConnection();
        super.onDestroy();
    }

    private void clearConnection() {
        signOut(false);
        if(mGoogleApiClient.isConnected()) {
            mGoogleApiClient.disconnect();
            mGoogleApiClient = null;
        }
    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        mNameTextView.setText(R.string.message_connection_failed);
    }

}