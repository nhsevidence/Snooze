﻿using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Snooze
{

    public class When_contructing_an_input : InputSpec
    {
        It Should_be_valid =()=> input.IsValid.ShouldBeTrue();
    }

    public class When_assigning_a_vaue_that_cannot_be_converted_to_an_input : InputSpec
    {
        Because of = () => input.RawValue = "bang";

        It Should_not_be_valid =()=> input.IsValid.ShouldBeFalse();

        It Should_have_an_error_message = () => FirstError.ShouldEqual("Invalid data.");
    }

    public class When_assigning_a_value_that_can_be_converted_to_an_input : InputSpec
    {
        Because of = () => input.RawValue = "42";

        It Should_have_converted_the_value = () => input.Value.ShouldEqual(42);
    }

    public class When_assigning_a_value_to_an_input_using_implict_conversion : InputSpec
    {
        Because of = () => input = 42;

        It Should_have_assigned_the_value = () => input.Value.ShouldEqual(42);
    }




    [Subject(typeof(Input<>))]
    public class InputSpec
    {
        protected static Input<int> input;
        Establish context =()=> input = new Input<int>();

        protected static string FirstError
        {
            get { return input.ErrorMessages.First(); }
        }
    }
}
