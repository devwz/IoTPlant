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
    public class IoTComponent : INotifyPropertyChanged
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

        public IoTComponent(string port)
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

                        if (plant.Solo == "Umido")
                        {
                            plant.Color = "#ADD8E6";
                        }
                        else if (plant.Solo == "Moderado")
                        {
                            plant.Color = "#98FB98";
                        }
                        else if (plant.Solo == "Seco")
                        {
                            plant.Color = "#F08080";
                        }

                        plant.ImgUrl = "Assets/parcialmente-nublado.png";
                        plant.Condicao = "Parcialmente nublado";

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
