using System;
using NLog.Web;
using System.IO;
using System.Collections;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

             string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            logger.Info(scrubbedFile);
            MovieFile movieFile = new MovieFile(scrubbedFile);

            string choice;
            do
            {
                Console.WriteLine("1) Read all movies from file.");
                Console.WriteLine("2) Add movie to file.");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();
                if (choice == "1"){
                     foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
                ///////////////////////
                 else if (choice == "2"){
                    string[] lines;
                    var movieID = new ArrayList();
                    var title = new ArrayList();
                    var genres = new ArrayList();

            using (StreamReader sr = new StreamReader(File.OpenRead("movies.scrubbed.csv")))
        {

            while (sr.EndOfStream == false)
            {
            string line = sr.ReadLine();
            lines = line.Split(',');
            movieID.Add(lines[0]);
            title.Add(lines[1]);
            genres.Add(lines[2]);

             
            }
        }
        int x=0;
        string newID="";
        string newTitle="";
        string final="";
        while(x ==0){
            int idTaken=0;
            int titleTaken=0;
            Console.WriteLine("Enter Movie ID: ");
             newID = Console.ReadLine();
            for(int a =0;a<movieID.Count;a++){
                if(newID.Equals(movieID[a])){
                    Console.WriteLine("That ID is already taken.");
                    idTaken=1;
                    titleTaken=1;
                    break;
                }
                else{}
            }
            if(idTaken==0){
                    Console.WriteLine("Enter Movie Title: ");
                     newTitle = Console.ReadLine();
                    for(int b=0;b<title.Count;b++){
                         if(newTitle.Equals(title[b])){
                            Console.WriteLine("That Title is already taken.");
                            titleTaken=1;
                            break;
                        }
                        else{}
                        }
            }
            if(titleTaken==0){
                x=1;
                final = newID+","+newTitle+",";
            }  
            
            }
            Console.WriteLine("Enter Genres each divided by a | and put a , at the end");
            final = final+Console.ReadLine();
            Console.WriteLine("Enter Director and put , at end");
            final = final+Console.ReadLine();
            Console.WriteLine("Enter run time, h:m:s");
            final = final+Console.ReadLine();
            Console.WriteLine(final);
             using (StreamWriter sw = File.AppendText("movies.scrubbed.csv")){
                            //sw.WriteLine();
                            sw.WriteLine(final);
                        }
                }
            } while (choice == "1" || choice == "2");

           

            logger.Info("Program ended");
        }
    }
}