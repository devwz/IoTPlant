using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlant.Models
{
    public class Component : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        SerialPort port;

        public IoTPlant plant = new IoTPlant();
        public IoTPlant Plant
        {
            get
            {
                return this.plant;
            }
            set
            {
                if (this.plant != value)
                {
                    this.plant = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Plant"));
                    }
                }
            }
        }

        public Component(string port)
        {
            this.port = new SerialPort(port, 9600);

            Task readData = new Task(ReadData);
            readData.Start();
        }

        void ReadData()
        {
            using (port)
            {
                port.Open();
                while (port.IsOpen)
                {
                    string result = port.ReadLine();

                    try
                    {
                        Plant = JsonConvert.DeserializeObject<IoTPlant>(result);
                    }
                    catch
                    {
                        Plant = new IoTPlant();
                    }
                }
            }
        }
    }
}
