using Android.Content;
using Android;
using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.OS;

namespace Notes.Models
{
     public class Notification: BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string title = intent.GetStringExtra("title");
            string message = intent.GetStringExtra("message");
 
        }
    }
}
