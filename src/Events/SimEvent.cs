namespace nvan.PoKeysConnector.Events
{
    public class SimEvent
    {
        public string name = "";
        public string dataRef = "";
        public InputOutput io = InputOutput.Input;
        public PinTypes pinType = PinTypes.PIN;
        public int pinNumber = 0, pin2Number = 0;
        public float readValue = 1f, writeValue = 1f;
        public float readMaxValue = 0f, writeMaxValue = 0f;
        public bool round = false;
        public bool syncOnStartup = false;
    }
}
