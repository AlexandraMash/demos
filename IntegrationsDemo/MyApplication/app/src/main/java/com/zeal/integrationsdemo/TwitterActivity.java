package com.zeal.integrationsdemo;

import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.twitter.sdk.android.Twitter;
import com.twitter.sdk.android.core.Callback;
import com.twitter.sdk.android.core.Result;
import com.twitter.sdk.android.core.TwitterAuthToken;
import com.twitter.sdk.android.core.TwitterException;
import com.twitter.sdk.android.core.TwitterSession;
import com.twitter.sdk.android.core.identity.TwitterLoginButton;
import com.zeal.integrationdemo.R;

public class TwitterActivity extends AppCompatActivity implements View.OnClickListener {
    private TwitterLoginButton mSignInButton;
    private Button mSignOutButton;
    private TextView mNameTextView;
    private TextView mTokenTextView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_twitter);

        mSignInButton = (TwitterLoginButton)findViewById(R.id.btn_sign_in);
        mSignOutButton = (Button)findViewById(R.id.btn_sign_out);
        mSignOutButton.setOnClickListener(this);
        mNameTextView = (TextView)findViewById(R.id.tv_name);
        mTokenTextView = (TextView)findViewById(R.id.tv_token);

        initTwitterLogin();

        TwitterSession session = Twitter.getSessionManager().getActiveSession();
        if(session != null) {
            setDataFromSession(session);
            setSignInUi(false);
        } else {
            setSignInUi(true);
        }
    }

    private void setSignInUi(boolean canSignIn) {
        mSignInButton.setEnabled(canSignIn);
        mSignOutButton.setEnabled(!canSignIn);
    }

    private void initTwitterLogin() {
        mSignInButton.setCallback(new Callback<TwitterSession>() {
            @Override
            public void success(Result<TwitterSession> result) {
                setDataFromSession(result.data);
                setSignInUi(false);
            }

            @Override
            public void failure(TwitterException exception) {
                mNameTextView.setText(getString(R.string.message_sign_in_failed, exception.getMessage()));
                mTokenTextView.setText("");
                setSignInUi(true);
            }
        });
    }

    private void setDataFromSession(@NonNull TwitterSession session) {
        TwitterAuthToken authToken = session.getAuthToken();
        mTokenTextView.setText(authToken.token);
        String name = session.getUserName();
        mNameTextView.setText(getString(R.string.message_signed_in_twitter, name));
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        mSignInButton.onActivityResult(requestCode, resultCode, data);
    }

    @Override
    public void onClick(View view) {
        switch(view.getId()) {
            case R.id.btn_sign_out:
                signOut();
                break;
        }
    }

    private void signOut() {
        Twitter.getSessionManager().clearActiveSession();
        Twitter.logOut();
        setSignInUi(true);
        mNameTextView.setText(R.string.message_not_logged_in);
        mTokenTextView.setText("");
    }
}
