using BillsManager;
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
   public  class FactureServiceImpl 
    {
        readonly SQLiteConnection database;
        public ObservableCollection<Facture> Factures { get; set; }
        public FactureServiceImpl(string dbPath)
        {
            //la crealtion de la base de donnees et l'etablissement de la conexion  
            database = DependencyService.Get<IFactureService>().GetConnection("StrategieRecouvrement.db3");
            database.CreateTable<Utilisateur>();
            // la création de la table facture
            database.CreateTable<Facture>();

            //Factures = new ObservableCollection<Facture>(database.TableAsync<Facture>());
            //si la table est vide je l'initialise
            
            if (database.Table<Facture>().Any())
            {
                AddnewFacture();
            }
            
        }
        public void AddnewFacture()
        {
            this.Factures.Add(new Facture
            {
                idFacture = "Fr123456",
                dateEcheanceFacture = new DateTime(2017, 8, 27),
                Client = "fati",
                montantTTC = 14782.36
            });
        }

        
        //la liste des Factures
        public IEnumerable<Facture> getListeFacture()
        {
            return database.Query<Facture>("SELECT * FROM FACTURE").AsEnumerable<Facture>();
        }

        //recuperer une facture a partir de son ident
        public Facture getFactByID(string idFact)
        {
            return database.Table<Facture>().FirstOrDefault(facture => facture.idFacture == idFact);
        }

        //Sauvegarder la facture apres modifs
        public void SaveFact(Facture facture)
        {
            if(facture.idFacture!= null)
            {
                database.Update(facture);
            }
            else
            {
                database.Insert(facture);
            }
        }

        //sauvegarder toutes les modifs
        public void saveAllFact()
        {
            foreach(var facture in this.Factures)
            {
                if(facture.idFacture != null)
                {
                    database.Update(facture);
                }
                else
                {
                    database.Insert(facture);
                }
            }
        }

        //supprimer facture pour l'instant on aura pas besoin de ce service
        public void deleteFact(Facture facture)
        {
            if (facture.idFacture != null)
            {
                database.Delete(facture);
            }
            this.Factures.Remove(facture);
        }

        //supprimer toutes les factures attention lors de l'appel de cette methode
        public void deleteAllFact()
        {
            database.DropTable<Facture>();
            database.CreateTable<Facture>();
            this.Factures = null;
            this.Factures = new ObservableCollection<Facture>(database.Table<Facture>());
        }
        
        
        /*
        //la liste des factures
        public Task<List<Facture>> getFacture()
        {
            return database.Table<Facture>().ToListAsync();
        }

        //retourne une facture
        public Task<Facture> getFactureAsync(string id)
        {
            return database.Table<Facture>().Where(i => i.idFacture == id).FirstOrDefaultAsync();
        }
        //l'ajout et la modification d'une facture
        public Task<int> SaveFactureAsync(Facture facture)
        {
            if (facture.idFacture == null)
            {
                return database.InsertAsync(facture);
            }else
            {
                return database.UpdateAsync(facture);
            }
        }
        
        //la suppression d'une facture
        public Task<int> deleteFactureAsync(Facture facture)
        {
            return database.DeleteAsync(facture);
        }
        */
       
    }
}
