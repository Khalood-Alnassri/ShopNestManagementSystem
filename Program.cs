namespace ShopNestManagementSystem
{
    internal class Program
    {

        public static void DisplayMenu()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("Welcome to the ShopNest Management System!");
            Console.WriteLine("==================================================");
            Console.WriteLine("1. Add a Physical Product.");
            Console.WriteLine("2. Add a Digital Product.");
            Console.WriteLine("3. Register a Customer.");
            Console.WriteLine("4. Place an Order.");
            Console.WriteLine("5. Display All Products.");
            Console.WriteLine("6. Display Customer Order History.");
            Console.WriteLine("7. Cancel an Order.");
            Console.WriteLine("8. Display Store Statistics.");
            Console.WriteLine("==================================================");
        }

        public static int SelectOption()
        {
            int option;

            while (true)
            {
                Console.Write("Please select an option: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, out option) && option >= 1 && option <= 8)
                {
                    return option;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 8.");
                }
            }

        }
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {

                DisplayMenu();
                int option = SelectOption();

                switch(option)
                {
                    case 1:
                        

                        break;

                    case 2:
                        break;

                    case 3:
                           break;

                    case 4:


                           break;

                    case 5:
                                
                           break;

                    case 6:

                           break;

                    case 7:

                           break;

                    case 8:

                           break;

                    default:

                        Console.WriteLine("Invalid option. Please select option from the list.");

                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

        }
    }

    // Product class represents a product in the system, which can be either physical or digital
    abstract class Product
    {
        protected string name; // all child classes can access this field
        protected double price; // all child classes can access this field
        private static int nextProductID = 100; // Static field to generate unique product IDs
        private static int totalProductsCreated;

        // property to only get the unique product ID
        public int GetProductID { get; }

        // property to only get the name of the product
        public string GetName { get; }

        // property to only get the price of the product
        public double GetPrice { get; }

        // property to set the price of the product 
        public void SetPrice (double value)
        {
            if (value < 0)
            {
                Console.WriteLine("Price cannot be negative. Setting price to 0.");
                price = 0;
            }
            else
            {
                price = value;
            }
        }

        // property to only get the total number of products created
        public static int GetTotalProductsCreated() 
        {
            return totalProductsCreated;
        }

        // Constructor to initialize the product with a unique ID, name, and price
        public Product(string name, double price)
        {
            this.name = name;
            SetPrice(price);
            GetProductID = nextProductID++;
            totalProductsCreated++;
        }

        // Must be overridden
        public abstract void DisplayInfo();

        // method to calculate total cost, can be overridden
        public virtual double CalculateTotalCost()   
        {
            return price;
        }
    }

    // PhysicalProduct class inherits from Product, representing a physical product in the system
    class PhysicalProduct : Product
    {

    }

    // DigitalProduct class inherits from Product, representing a digital product in the system
    class DigitalProduct : Product
    {

    }

    // User class represents a user in the system, which can be a customer or an admin
    abstract class User
    {

    }

    // Customer class inherits from User, representing a customer in the system
    class Customer : User
    {
    }

    // Admin class inherits from User, representing an administrator in the system
    class Admin : User
    {
        
    }
}
