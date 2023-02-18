using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GameCore.Spec
{
    [Binding]
   public class Hooks:Steps
    {
        [BeforeScenario()]
        public void BeforeScenerio()
        {

        }
        [AfterScenario()]
        public void AfterScenerio()
        {

        }
    }
}
