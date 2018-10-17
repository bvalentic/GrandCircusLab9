using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GrandCircusLab9
{
    class Program
    {        
        static void Main(string[] args)
        {
            /* provide information about students in class
             * prompt user to ask about particular student (by number)
             * give proper responses according to user-provided info
             * ask if user would like to learn about another student
             */

            List<string> students = new List<string>
            {
                //list of students (alphabetical)
                "Abby Wessels",
                "Blake Shaw",
                "Bob Valentic",
                "Jay Stiles",
                "John Shaw",
                "Jordan Owiesny",
                "Joshua Zimmerman",
                "Lin-Z Chang",
                "Madelyn Hilty",
                "Michael Hern",
                "Nana Banahene",
                "Rochelle \"Roach\" Toles",
                "Shah Shahid",
                "Taylor Everts",
                "Tim Broughton"
            };

            List<string> food = new List<string>
            {
                //list of favorite foods, sorted by student
                "soup",
                "cannolis",
                "pizza",
                "pickles",
                "ribs",
                "burgers",
                "turkey",
                "ice cream",
                "croissants",
                "chicken wings",
                "bananas",
                "space cheese",
                "chicken wings",
                "chicken Cordon Bleu",
                "chicken parmesan"
            };

            List<string> town = new List<string>
            {
                //list of hometowns, sorted by student
                "Traverse City, MI",
                "Los Angeles, CA",
                "St. Clair Shores, MI",
                "Macomb, MI",
                "Huntington Woods, MI",
                "Warren, MI",
                "Taylor, MI",
                "Toledo, OH",
                "Oxford, MI",
                "Canton, MI",
                "Ghana",
                "Mars",
                "Newark, NJ",
                "Caro, MI",
                "Detroit, MI"
            };

            List<string> color = new List<string>
            {
                //list of colors, sorted by student
                "cerulean",
                "burnt orange",
                "forest green",
                "apricot",
                "violet red",
                "robin egg blue",
                "periwinkle",
                "chestnut",
                "indigo",
                "goldenrod",
                "purple mountains majesty",
                "tickle me pink",
                "yellow green",
                "wisteria",
                "sepia"
            };

            Console.WriteLine("Hello, and welcome to the Dev.Build(2.0) database!");
            bool goAgain = true;
            //allows looping until user decides to quit
            while (goAgain)
            {
                FindInfo(students, food, town, color);
                goAgain = KeepGoing("Would you like to continue? (y/n) ");
            }


        }
        static bool KeepGoing(string message)
        {//method to check if user wants to continue, returns boolean used as check
            bool correctInput = true; //makes sure user puts in a variation of "yes" or "no"
            bool continuer = true; //eventual returned boolean
            do
            {
                Console.Write("\n" + message);
                string confirm = Console.ReadLine().ToLower();
                if (confirm == "n" || confirm == "no")
                {
                    Console.WriteLine("Come back soon!");
                    continuer = false;
                    correctInput = true;
                    Console.ReadKey();
                }
                else if (confirm == "y" || confirm == "yes")
                {
                    Console.WriteLine("\nOkay!\n");
                    continuer = true;
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("Sorry, I didn't understand.");
                    correctInput = false;
                }
            } while (!correctInput);
            return continuer;
        }

        static void FindInfo(List<string> students, List<string> food,List<string> town, List<string> color)
        {
            //Console.Write("Input a number: ");
            //int input = int.Parse(Console.ReadLine());
            //Console.WriteLine($"{students[input]} is from {town[input]}, likes eating {food[input]}, and their favorite color is {color[input]}." );

            Console.WriteLine("Enter the number that corresponds to the team member you wish to look up. \n" +
                    "To add a team member, enter \"add\". \n" +
                    "For a list of team members, enter \"0\".");
            int inputName;
            bool loopChoice = true;
            string inputString = Console.ReadLine();
            while (!int.TryParse(inputString, out inputName) && inputString != "add")
            //catches exceptions if input isn't an int
            {
                Console.Write("I'm sorry, I didn't understand. Please try again: ");
                inputString = Console.ReadLine();
            }
            if (inputString == "add")
            {
                AddToList(students, food, town, color);
            }
            else if (inputName == 0)
            {//print list
                PrintList(students);
            }
            else
            {//move on to rest of database               
                {
                    try
                    {
                        
                        int newIndex = GetIndex(students,students[inputName-1]);
                        
                        Console.WriteLine($"\n{inputName} is {students[newIndex]}. Would you like to know more?");
                        do
                        {
                            loopChoice = true; //loops for current user selection
                            Console.WriteLine("(Press \"0\" to skip, \"1\" for favorite food, \"2\" for hometown, or \"3\" for favorite color)\n");
                            try
                            {
                                int inputNum = int.Parse(Console.ReadLine());
                                //var test = nameArray[inputName, inputNum]; //this tests to see if input is correct
                                if (inputNum == 0)
                                {
                                    //option to "quit" and go back to the list of names
                                    Console.WriteLine("Going back to the main database. . .");
                                    loopChoice = false;
                                }
                                else if (inputNum == 1)
                                {
                                    //show food
                                    Console.WriteLine($"{students[newIndex]}'s favorite food is {food[newIndex]}.");
                                    Console.WriteLine("Would you like to know more?");
                                }
                                else if (inputNum == 2)
                                {
                                    //show hometown
                                    Console.WriteLine($"{students[newIndex]}'s hometown is {town[newIndex]}.");
                                    Console.WriteLine("Would you like to know more?");
                                }
                                else if (inputNum == 3)
                                {
                                    //show color
                                    Console.WriteLine($"{students[newIndex]}'s favorite color is {color[newIndex]}.");
                                    Console.WriteLine("Would you like to know more?");
                                }
                                else Console.WriteLine("I'm sorry, that is not an acceptable choice.");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("I'm sorry, I don't understand. Please input a number corresponding to your selection.");
                            }
                        } while (loopChoice);
                    } //two sets of exception catches for each nested loop
                      //otherwise it will catch the exception but also exit the loop
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("I'm sorry, that input is not in the acceptable range.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("I'm sorry, I don't understand. Please input a number corresponding to your selection.");
                    }
                }
            }                
        }
        
        static List<string> AddNameList (List<string> list, string addedString)
        {//adds string to student list and sorts alphabetically
            list.Add(addedString);
            list.Sort();
            return list;
        }

        static int GetIndex(List<string> mainList, string mainString)
        {//returns desired index for list based on main (name) list
            return mainList.IndexOf(mainString);
        }
        
        static List<string> UpdateList (List<string> list, int index, string updateString)
        {//add entry to list based on index provided
            list.Insert(index, updateString);
            return list;
        }

        static void PrintList(List<string> list)
        {//prints neatly-formatted list of input numbers and names
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}: {list[i]}"); //formatted for easier readability
            }
        }

        static string StringNameAdder(string message, List<string> stringList)
        {//checks for an entry (that isn't blank), then adds that entry to the main name list
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (Regex.IsMatch(input, @"\S"))//only proceed if there is an entry
                {
                    AddNameList(stringList, input);
                }
                else
                {
                    Console.WriteLine("Please make sure to actually enter a word or phrase before hitting \"Enter\"");
                }
            } while (!Regex.IsMatch(input, @"\S"));
            return input;
        }

        static void StringOtherAdder(string message, int index, List<string> stringList)
        {// adds a string to the list 
         //(for the other lists besides student name)
         //(this method requires an index in order to match it to the corresponding name index)
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (Regex.IsMatch(input, @"\S"))//only proceed if there is an entry
                {
                    UpdateList(stringList, index, input);
                }
                else
                {
                    Console.WriteLine("Please make sure to actually enter a word or phrase before hitting \"Enter\"");
                }
            } while (!Regex.IsMatch(input, @"\S"));            
        }

        static void AddToList(List<string> students, List<string> food, List<string> town, List<string> color)
        {//adding student to list(s): prompts for each entry and adds input to each list (name, food, town, and color)
            
            string newName = StringNameAdder("Enter the name of the person you wish to add: ", students);        
            int newIndex = GetIndex(students, newName);//this is needed for the other lists to match with the names

            StringOtherAdder("Enter the person's favorite food: ", newIndex, food);

            StringOtherAdder("Enter the person's hometown: ", newIndex, town);

            StringOtherAdder("Enter the person's favorite color: ", newIndex, color);

            Console.WriteLine($@"
                    Thank you! We have received the following input:
                    name: {students[newIndex]}
                    favorite food: {food[newIndex]}
                    hometown: {town[newIndex]}
                    favorite color: {color[newIndex]}
                    ");
        }       
    }
}
