﻿using Machine.Specifications;
using SampleApplication.Controllers;
using Snooze.MSpec;
using Snooze.Routing;

namespace Snooze
{
	public class RoutableHandler : Handler
	{

		static Register route = r => r.Map<Command>(u => "commandhandler/" + u.stupid)
			;

		public class StupidType
		{
			public string Value { get; set; }
		}

		public class Command : Url
		{
			public StupidType stupid { get; set; }
		}

		public ResourceResult Get(Command cmd) { return OK(cmd); }
	}

	public class handlers_are_routable : with_controller<RoutableHandler.Command, RoutableHandler>
	{
		Establish context = () =>
		                    {

		                    };

		Because of = () => get("commandhandler/stupid");

		It is_routable = is_200;
	}

	public class RenderingViews : with_controller<HomeViewModel,HomeController>
	{
		Establish view_location = () => application_under_test_is_here("../SampleApplication");

		Because of = () => get("");

		It content_negotiates_texthtml = () => conneg_html()
			.DocumentNode
			.ShouldNotBeNull();
	}
}