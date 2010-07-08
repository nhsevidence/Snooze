﻿using System;
using System.Web.Mvc;

namespace Snooze
{
    public class StaticFileUrl : Url
    {
        public string Version { get; set; }
        public string Path { get; set; }
    }

    public class StaticFileController : ResourceController
    {
        public void Get(StaticFileUrl url)
        {
            var filename = Server.MapPath("~/" + url.Path);
            if (!System.IO.File.Exists(filename))
            {
                Response.StatusCode = 404;
                return;
            }

            if (HttpContext.IsDebuggingEnabled == false)
            {
                // Effectively tell the client to cache this forever.
                Response.Cache.SetExpires(DateTime.UtcNow.AddYears(10));
            }

            Response.ContentType = MimeTypes.GetMimeTypeForFilename(filename);
            Response.TransmitFile(filename);
        }
    }
}
