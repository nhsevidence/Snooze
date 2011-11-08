﻿#region

using System.Web.Mvc;
using System.Web.Script.Serialization;
using Glue;

#endregion

namespace Snooze
{

	public class ProjectingJsonSerialiser<L,R>
	{
		readonly L model;
		readonly R projection;

		public ProjectingJsonSerialiser(L model, R projection)
		{
			this.model = model;
			this.projection = projection;
		}
	}

    public class JsonFormatter : IResourceFormatter
    {
        #region IResourceFormatter Members

        public bool CanFormat(ControllerContext context, object resource, string mimeType)
        {
            return resource != null && mimeType == "application/json";
        }

        public void Output(ControllerContext context, object resource, string contentType)
        {
            var s = new JavaScriptSerializer();
            s.RegisterConverters(new[] {new UrlConverter()});
            var json = s.Serialize(resource);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Output.Write(json);
        }

        #endregion
    }
}