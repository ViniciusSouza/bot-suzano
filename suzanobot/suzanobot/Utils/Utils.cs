using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suzanobot.Utils
{
    public static class Utils
    {

        public static UriBuilder GetPlaceHoldImg(int size, int width, int height, string text)
        {
            UriBuilder uri = new UriBuilder("https", "placeholdit.imgix.net");
            uri.Path = "~text";
            uri.Query = string.Format("txtsize={0}&txt={1}&w={2}&h={3}", size, text, width, height);
            return uri;
        }

       

    }
}