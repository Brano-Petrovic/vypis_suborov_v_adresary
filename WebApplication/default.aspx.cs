using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;

namespace WebApplication
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void zobrazit(object sender, EventArgs e)
        {            
            string path_folder = input_file_path.Text;      //Nacitanie celej cesty k adresaru z inputboxu
            List<string> actual_files;                      
            
            try // kontrola ci uzivatel zadal existujucu cestu k adresaru
            {
                actual_files = Directory.GetFiles(path_folder, "*", SearchOption.AllDirectories).ToList(); // Nacitanie vsetkych nazvov suborov ktore su v danom adresary
            }
            catch                                                                           
            {
                old_files.path = null;
                vypis_aktualny.Text = "Hľadaný priečinok neexistuje!";      // V pripade ak je cesta neexistujuca, vypise upozornenie a dalej nepokracuje.
                return;
            };
            
            List<DateTime> actual_file_dates = new List<DateTime>();                            //vytvorenie prazdnych pomocnych listov do ktorych ulozi udaje o novo nacitanych suboroch
            List<int> actual_file_versions = new List<int>();                                   
            for (var i = 0; i < actual_files.Count; i++)
            {
                actual_files[i] = actual_files[i].Remove(0, path_folder.Length);                // v cykle prejdem vsetky subory ktore som pred tym nacital do listu "actual_files"
                actual_file_dates.Add(File.GetLastWriteTime(@path_folder + actual_files[i]));   // do dalsich listov nasledne zapisem posledny datum kedy bol subor modifikovany
                actual_file_versions.Add(1);                                                    // a kazdemu priradim na zaciatku cislo verzie 1
            }   
            
            if (String.IsNullOrEmpty(old_files.path) || old_files.path != path_folder)          // overenie ci bol dany adresar spusteny 1.krat a len sa zapisu ziskane udaje 
            {                                                                                   // alebo je to uz dalsie spustenie a moze porovnat ci boli vykonane nejake zmeny v adresary
                vypis_aktualny.Text = "Nový adresař, žádne změny.";
            }
            else
            {
                vypis_aktualny.Text = "";
                for (var i = 0; i < actual_files.Count; i++)                                    // 1. cyklus prejde cely list actual_files aby nasiel pridane subory
                {           
                    if (!old_files.names.Contains(actual_files[i]))                             // ak povodny list suborov neobsahuje novo nacitany subor, tak to znamena, ze je to novo pridany subor
                    {                                                                           // v tom pripade vypise tento subor do labelu na stranke
                        vypis_aktualny.Text += "[A]" + actual_files[i] + "<br>";
                    }
                }

                for (var i = 0; i < actual_files.Count; i++)                                    //2. cyklus prejde cely list actual_files aby nasiel upravene subory
                {
                    if (old_files.names.Contains(actual_files[i]))                              //Ak povodny list obsahuje novo nacitany subor,
                    {                                                                               
                        var index = old_files.names.FindIndex(x => x == actual_files[i]);       // tak nasledne zisti na ktorom indexe sa nachadza
                        if (old_files.versions[index] > 1)                                      // skontroluje verziu suboru
                        {                                                                       // ak je vacsia ako 1, tak si ju zapise do aktualneho zoznamu
                            actual_file_versions[i] = old_files.versions[index];
                        }
                        if (old_files.dates[index] != actual_file_dates[i])                     //porovna zapisany datum zmeny pri predchadzajucom pusteni programu s aktualnym
                        {
                            actual_file_versions[i]++;                                          // Ak su odlisne, tak zvysi verzie o 1 a do labalu na stranke vypise nazov suboru
                            vypis_aktualny.Text += "[M]" + actual_files[i] + "(verzia " + actual_file_versions[i] + ")<br>";
                        }
                        
                    }
                }

                for (var i = 0; i < old_files.names.Count-1; i++)                               //3. cyklus porovna povodny zoznam s novo nacitanym a zisti ktore subory boli zmazane
                {
                    if (!actual_files.Contains(old_files.names[i]))                             //Ak predtym nacitany subor sa nenachadza medzi prave nacitanymi tak to znamena,
                    {                                                                           //ze subor bol zmazany. 
                        vypis_aktualny.Text += "[D]" +old_files.names[i] + "<br>";              //Takze nazov suboru sa vypise na stranke
                    }
                }

                if (vypis_aktualny.Text == "")                                                  //podla zadania, ak sa nic nezmeni v adresary, tak sa na label vypise iba "žádna změna"
                {
                    vypis_aktualny.Text = "žádna změna";
                }     
            }
            

            old_files.path = path_folder;                                                       // Na konci sa aktualizuje zoznamy so vsetkymi udajmi o suboroch
            old_files.names = actual_files;
            old_files.dates = actual_file_dates;
            old_files.versions = actual_file_versions;
            return;
        }
    }
}