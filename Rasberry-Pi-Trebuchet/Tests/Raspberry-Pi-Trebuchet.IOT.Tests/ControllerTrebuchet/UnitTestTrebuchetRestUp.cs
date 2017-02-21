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
    public class TrebuchetRestUpRunTests
    {

        [TestMethod]
        public void TrebuchetTest_Reset()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();
            restRouteHandler.RegisterController<TrebuchetController>();

            var Isrunning = UltraSonicRunTestHelper.IsUltraSonicRunning(restRouteHandler);
            int LoopUntil = 0;
            while (Isrunning && LoopUntil < 10)
            {
                Task.Delay(1000).Wait();
                Isrunning = UltraSonicRunTestHelper.IsUltraSonicRunning(restRouteHandler)
                LoopUntil++;
            }

            //Make sure the ultra Sonic is not running
            var SuccessfullyResetTrebuchet = TrebuchetTestHelper.ResetTrebuchet(restRouteHandler);
            Assert.IsTrue(SuccessfullyResetTrebuchet, "Could not Successfully Reset the Trebuchet");
        }

        public void TrebuchetLaunch()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();
            restRouteHandler.RegisterController<TrebuchetController>();
        }


    }
}
