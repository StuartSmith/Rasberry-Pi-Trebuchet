using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Trebuchet.Controllers.api;
using Raspberry_Pi_Trebuchet.Tests.IOT.ControllerSonic;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Tests.IOT.ControllerTrebuchet
{
    [TestClass]
    public class UnitTestTrebuchetController
    {
        [TestMethod]
        public void TrebuchetTest_Fire()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();
            restRouteHandler.RegisterController<TrebuchetController>();

            TrebuchetTestHelper.WaitForTrebuchetToFinishRunning(restRouteHandler, 10);
            var SuccessfullyFiredTrebuchet = TrebuchetTestHelper.FireTrebuchet(restRouteHandler);

            Assert.IsTrue(SuccessfullyFiredTrebuchet, "Could not Successfully Reset the Trebuchet");

        }


        [TestMethod]
        public void TrebuchetTest_Fire_Reset_Fire()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();
            restRouteHandler.RegisterController<TrebuchetController>();

            TrebuchetTestHelper.WaitForTrebuchetToFinishRunning(restRouteHandler, 10);
            var SuccessfullyFiredTrebuchet = TrebuchetTestHelper.FireTrebuchet(restRouteHandler);
            Assert.IsTrue(SuccessfullyFiredTrebuchet, "Could not Successfully Reset the Trebuchet");

            TrebuchetTestHelper.WaitForTrebuchetToFinishRunning(restRouteHandler, 10);
            var SuccessfullyResetTrebuchet = TrebuchetTestHelper.ResetTrebuchet(restRouteHandler);
            Assert.IsTrue(SuccessfullyResetTrebuchet, "Could not Successfully Reset the Trebuchet");

            TrebuchetTestHelper.WaitForTrebuchetToFinishRunning(restRouteHandler, 10);
            SuccessfullyFiredTrebuchet = TrebuchetTestHelper.FireTrebuchet(restRouteHandler);
            Assert.IsTrue(SuccessfullyFiredTrebuchet, "Could not Successfully Reset the Trebuchet");
        }


        [TestMethod]
        public void TrebuchetTest_Reset()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();
            restRouteHandler.RegisterController<TrebuchetController>();
            TrebuchetTestHelper.WaitForTrebuchetToFinishRunning(restRouteHandler, 10);

            //Make sure the ultra Sonic is not running
            var SuccessfullyResetTrebuchet = TrebuchetTestHelper.ResetTrebuchet(restRouteHandler);
            Assert.IsTrue(SuccessfullyResetTrebuchet, "Could not Successfully Reset the Trebuchet");
        }
        
    }
}
