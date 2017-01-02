package com.zeal.integrationsdemo;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.zeal.integrationdemo.R;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_main);
        Button twitterButton = (Button)findViewById(R.id.twitter_button);
        twitterButton.setOnClickListener(this);
        Button facebookButton = (Button)findViewById(R.id.facebook_button);
        facebookButton.setOnClickListener(this);
        Button googleButton = (Button)findViewById(R.id.google_button);
        googleButton.setOnClickListener(this);
        Button youTubeButton = (Button)findViewById(R.id.youtube_button);
        youTubeButton.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.twitter_button:
                Intent twitterIntent = new Intent(this, TwitterActivity.class);
                startActivity(twitterIntent);
                break;
            case R.id.facebook_button:
                Intent facebookIntent = new Intent(this, FacebookLoginActivity.class);
                startActivity(facebookIntent);
                break;
            case R.id.google_button:
                Intent googleIntent = new Intent(this, GooglePlusLoginActivity.class);
                startActivity(googleIntent);
                break;
            case R.id.youtube_button:
                Intent youtubeIntent = new Intent(this, YouTubeActivity.class);
                startActivity(youtubeIntent);
                break;
        }
    }

}
