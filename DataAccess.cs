using BillsManager.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BillsManager.service
{
    public class DataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Utilisateur> Users { get; set; }
        public ObservableCollection<Facture> Bills { get; set; }

        public DataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            database.CreateTable<Utilisateur>();
            database.CreateTable<Facture>();

            this.Users = new ObservableCollection<Utilisateur>(database.Table<Utilisateur>());
            this.Bills = new ObservableCollection<Facture>(database.Table<Facture>());

            //if the table is empty fill it with some Data
            if (database.Table<Utilisateur>().Any())
            {
                AddnewUser();
            }
            if (database.Table<Facture>().Any())
            {
                AddnewBill();
            }
        }

        public void AddnewUser()
        {
            this.Users.Add(new Utilisateur
            {
                UserName="faty",
                Password="123"

            });
            this.Users.Add(new Utilisateur
            {
                UserName="Soufiane",
                Password="123"
            });
        }

        public void AddnewBill()
        {
            this.Bills.Add(new Facture
            {
                idFacture = "ABCD",
                Client = "canal food",
                montantTTC = 147852.2,
                dateEcheanceFacture = new DateTime(2017, 8, 27)//yyyy,MM,DD

            });
            this.Bills.Add(new Facture
            {
                idFacture = "EFGH",
                Client = "LGMZ",
                montantTTC = 147852.2,
                dateEcheanceFacture = new DateTime(2018, 8, 27)//yyyy,MM,DD

            });
            this.Bills.Add(new Facture
            {
                idFacture = "IJKL",
                Client = "LGSZ",
                montantTTC = 147852.2,
                dateEcheanceFacture = new DateTime(2019, 8, 27)//yyyy,MM,DD

            });
        }
        public IEnumerable<Utilisateur> GetUser(string username,string password)
        {
           lock (collisionLock)
            {
                var querry = from user in database.Table<Utilisateur>()
                             where user.UserName == username && user.Password == password
                             select user;
                return querry.AsEnumerable();
            }
        }

        public IEnumerable<Facture> GetBill(string idFact)
        {
            //Lock object is used to avoid database collision
            lock (collisionLock)
            {
                var querry = from bill in database.Table<Facture>()
                             where bill.idFacture == idFact
                             select bill;
                return querry.AsEnumerable();
            }
        }

        public IEnumerable<Facture> GetBills()
        {
            lock (collisionLock)
            {
                return database.Query<Facture>("select * from Bills").AsEnumerable();
            }
        }
        //
        //Actually we will use just the update method cuz we import the list of bills, we don't create any bill
        // 
        public Facture SaveBill(Facture facture)
        {
            lock (collisionLock)
            {
                if (facture.idFacture != null)//this bill already exists 
                {
                    database.Update(facture);
                }
                else
                {
                    database.Insert(facture);
                }
                return facture;
            }
        }

        //to save all the items in the collection 
        public void SaveAllBills()
        {
            lock (collisionLock)
            {
                foreach(var bill in this.Bills)
                {
                    if (bill.idFacture != null)
                    {
                        database.Update(bill);
                    }
                    else
                    {
                        database.Insert(bill);
                    }
                }
            }
        }

        public void DeleteBill(Facture facture)
        {
            var id = facture.idFacture;
            if (id != null)
            {
                lock (collisionLock)
                {
                    database.Delete(facture);
                }
            }
            this.Bills.Remove(facture);
        }

        //by any chance u want to drop the list of bills nd recreate it again be careful while using this method
        public void DeleteAllBills()
        {
            lock (collisionLock)
            {
                database.DropTable<Facture>();
                database.CreateTable<Facture>();
            }
            this.Bills = null;
            this.Bills = new ObservableCollection<Facture>(database.Table<Facture>());
        }


    }
}
