using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rbmqtest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            /*
            string type = Console.ReadLine();
            //生产者
            if (type == "1")
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "127.0.0.1";
                //默认端口
                factory.Port = 5672;
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel channel = conn.CreateModel())
                    {
                        //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                        channel.QueueDeclare("MyRabbitMQ", true, false, false, null);
                        while (true)
                        {
                            string message = string.Format("Message_{0}", Console.ReadLine());
                            byte[] buffer = Encoding.UTF8.GetBytes(message);
                            IBasicProperties properties = channel.CreateBasicProperties();
                            properties.DeliveryMode = 2;
                            channel.BasicPublish("", "MyRabbitMQ", properties, buffer);
                            Console.WriteLine("消息发送成功：" + message);
                        }
                    }
                }
            }
            else
            {
                int count = 1;
                //消费者
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "127.0.0.1";
                //默认端口
                factory.Port = 5672;
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel channel = conn.CreateModel())
                    {
                        //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                        channel.QueueDeclare("MyRabbitMQ", true, false, false, null);

                        //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                        channel.BasicQos(0, 1, false);

                        Console.WriteLine("Listening...");

                        //在队列上定义一个消费者
                        QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                        //消费队列，并设置应答模式为程序主动应答
                        channel.BasicConsume("MyRabbitMQ", false, consumer);

                        while (true)
                        {
                            //阻塞函数，获取队列中的消息
                            BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                            byte[] bytes = ea.Body;
                            string str = Encoding.UTF8.GetString(bytes);

                            Console.WriteLine("队列消息:"+ count.ToString()+". " + str.ToString());
                            count++;

                            //Thread.Sleep(20000);
                            //回复确认
                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                    }
                }
            }
            */
            #endregion

            Person person = new Person();

            person.age = 18;

            person.name = "tom";

            person.secret = "i will not tell you";

            FileStream stream = new FileStream(@"c:\temp\person.dat", FileMode.Create);

            BinaryFormatter bFormat = new BinaryFormatter();

            bFormat.Serialize(stream, person);

            stream.Close();

            Person person1 = new Person();

            FileStream stream1 = new FileStream(@"c:\temp\person.dat", FileMode.Open);

            BinaryFormatter bFormat1 = new BinaryFormatter();

            person1 = (Person)bFormat1.Deserialize(stream1);//反序列化得到的是一个object对象.必须做下类型转换

            stream1.Close();

            Console.WriteLine(person1.age + person1.name + person1.secret);//结果为18tom.因为secret没有有被序列化.

            Console.ReadLine();

        }
    }

    [Serializable] //如果要想保存某个class中的字段,必须在class前面加个这样attribute(C#里面用中括号括起来的标志符)
    public class Person
    {
        public int age;

        public string name;

        //[NonSerialized] //如果某个字段不想被保存,则加个这样的标志

        public string secret;

    }
}

