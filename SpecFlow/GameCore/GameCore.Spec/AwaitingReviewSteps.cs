using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;


namespace GameCore.Spec
{
    [Binding]
    [Scope(Tag = "AwaitingReviewBeforeStartingWork")]
    public class AwaitingReviewSteps
    {
        [Given(".*")]
        [Then(".*")]
        [When(".*")]
        public void Empty()
        {

        }
    }

}
