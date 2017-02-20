using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.RetupHttpRequests;
using Restup.HttpMessage.Models.Schemas;
using Restup.Webserver.Rest;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Tests.IOT.ControllerSonic
{
    [TestClass]
    public class UnitTestSonicController
    {
        [TestMethod]
        public void UltraSonicTest_StartUltraSonicRun()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();

            var Isrunning = UltraSonicRunTestHelper.IsUltraSonicRunning(restRouteHandler);
            Assert.IsFalse(Isrunning);

            var RunHasStarted = UltraSonicRunTestHelper.StartUltraSonicRun(restRouteHandler);
            Assert.IsTrue(RunHasStarted);

            Task.Delay(2000).Wait();
            Isrunning = UltraSonicRunTestHelper.IsUltraSonicRunning(restRouteHandler);
            while (Isrunning)
            {
                Isrunning = UltraSonicRunTestHelper.IsUltraSonicRunning(restRouteHandler);
                Task.Delay(2000).Wait();
            }


            Assert.IsFalse(Isrunning);
        }

        [TestMethod]
        public void UltraSonicTest_RemoveUltraSonicRun()
        {
            //clear Test remove all run
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();
            var DeleteUltraSonicRunRequest = HttpRequestsSonic.DeleteRequest_RemoveAllUltraSonicRuns();
            var request = restRouteHandler.HandleRequest(DeleteUltraSonicRunRequest);

            //Create an ultra sonic run 
            UltraSonicTest_StartUltraSonicRun();
            
            //Removed one or more ultra sonic run 
            request = restRouteHandler.HandleRequest(DeleteUltraSonicRunRequest);
            Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.OK);
            //No Ultra sonic runs left to remove
            request = restRouteHandler.HandleRequest(DeleteUltraSonicRunRequest);
            Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.NoContent);
        }

        [TestMethod]
        public void UltraSonicTest_RetrieveUltraSonicRun()
        {
            //Remove all Ultra sonic test data 
            UltraSonicTest_RemoveUltraSonicRun();

            //Create some ultra sonic test data 
            UltraSonicTest_StartUltraSonicRun();

            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();
            var GetAllUltraSonicRunRequest = HttpRequestsSonic.GetRequest_AllUltraSonicRuns();
            var request = restRouteHandler.HandleRequest(GetAllUltraSonicRunRequest);

            var ultraSonicRuns = UltraSonicRunTestHelper.DeserializedUltraSonicRuns(request.Result);
            Assert.AreEqual(ultraSonicRuns.Count, 1, "Only one Ultra Sonic run count should exist.");    

        }

       



       
    }
}
