namespace VehicleParkTests
{
    using VehiclePark.Engine;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestCommand
    {
        [TestMethod]
        public void CommandNameShouldBeGosho()
        {
            string command = "Gosho {\"age\": 20, \"height\": 72}";
            Command cmd = new Command(command);
            Assert.AreEqual("Gosho", cmd.Name, "Command name should be Gosho!");
        }

        [TestMethod]
        public void CommandParamShouldBe20()
        {
            string command = "Gosho {\"age\": 20}";
            Command cmd = new Command(command);
            Assert.AreEqual(20, int.Parse(cmd.Parameters["age"]), "Command parameter should be \"age\"!");
        }
    }
}
