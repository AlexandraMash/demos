package com.zeal.integrationsdemo;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.TextView;

import com.facebook.AccessToken;
import com.facebook.AccessTokenTracker;
import com.facebook.CallbackManager;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.Profile;
import com.facebook.ProfileTracker;
import com.facebook.login.LoginBehavior;
import com.facebook.login.LoginResult;
import com.facebook.login.widget.LoginButton;
import com.zeal.integrationdemo.R;

public class FacebookLoginActivity extends Activity {

    private LoginButton mSignInButton;
    private TextView mNameTextView;
    private TextView mTokenTextView;

    private CallbackManager mCallbackManager;
    private ProfileTracker mProfileTracker;
    private AccessTokenTracker mAccessTokenTracker;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_facebook);

        mSignInButton = (LoginButton)findViewById(R.id.btn_sign_in);
        mNameTextView = (TextView)findViewById(R.id.tv_name);
        mTokenTextView = (TextView)findViewById(R.id.tv_token);

        mCallbackManager = CallbackManager.Factory.create();
        mSignInButton.setReadPermissions("public_profile", "email");
        mSignInButton.setLoginBehavior(LoginBehavior.WEB_ONLY);

        setInitialData();

        initProfileTracker();
        initAccessTokenTracker();

        mSignInButton.registerCallback(mCallbackManager, mCallback);
    }

    private void initProfileTracker() {
        mProfileTracker = new ProfileTracker() {
            @Override
            protected void onCurrentProfileChanged(Profile oldProfile, Profile currentProfile) {
                setProfileData(currentProfile);
            }
        };
        mProfileTracker.startTracking();
    }

    private void setProfileData(Profile profile) {
        if(profile != null) {
            String name = profile.getName();
            mNameTextView.setText(getString(R.string.message_signed_in_facebook, name));
        } else {
            mNameTextView.setText(R.string.message_not_logged_in);
        }
    }

    private void initAccessTokenTracker() {
        mAccessTokenTracker = new AccessTokenTracker() {
            @Override
            protected void onCurrentAccessTokenChanged(AccessToken oldAccessToken, AccessToken currentAccessToken) {
                setToken(currentAccessToken);
            }
        };
        mAccessTokenTracker.startTracking();
    }

    private void setToken(AccessToken token) {
        if(token == null) {
            mTokenTextView.setText("");
        } else if(token.isExpired()) {
            mTokenTextView.setText(R.string.message_token_expired);
        } else {
            mTokenTextView.setText(token.getToken());
        }
    }

    private void setInitialData() {
        AccessToken token = AccessToken.getCurrentAccessToken();
        setToken(token);
        Profile profile = Profile.getCurrentProfile();
        setProfileData(profile);
    }

    private FacebookCallback<LoginResult> mCallback = new FacebookCallback<LoginResult>() {
        @Override
        public void onSuccess(LoginResult loginResult) {
            String idTokenString = loginResult.getAccessToken().getToken();
            mTokenTextView.setText(idTokenString);
        }

        @Override
        public void onCancel() {
            mNameTextView.setText(R.string.message_cancelled);
            mTokenTextView.setText("");
        }

        @Override
        public void onError(FacebookException exception) {
            mNameTextView.setText(getString(R.string.message_sign_in_failed,
                    exception.getMessage()));
            mTokenTextView.setText("");
        }
    };

    @Override
    protected void onDestroy() {
        super.onDestroy();
        mProfileTracker.stopTracking();
        mAccessTokenTracker.stopTracking();
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        mCallbackManager.onActivityResult(requestCode, resultCode, data);
    }
}
