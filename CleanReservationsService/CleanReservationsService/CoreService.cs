using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CleanReservationsService
{
    public partial class CoreService : ServiceBase
    {
        public CoreService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer();
            //Toutes les 1 minutes
            timer.Interval = 60000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            //Règle métier: Si on est vendredi 11h00
            if((int)currentTime.DayOfWeek == 5 && currentTime.Hour == 11 && currentTime.Minute < 1)
            {
                CleanReservations();
            }
        }

        private void CleanReservations()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=.;Database=GestionPlacesParking;Trusted_Connection=True;"))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE Reservation SET IsReserved = 0";
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
