using ShopNestManagementSystem;
using System.Security.Cryptography.X509Certificates;

namespace ShopNestManagementSystem
{
    internal class Program
    {
        public static Store shop = new Store("ShopNest");
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

        // case 1: function to add a physical product 
        public static void  AddPhysicalProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter product price: ");
            double price = double.Parse(Console.ReadLine() ?? string.Empty);

            Console.WriteLine("Enter product weight: ");
            double weight = double.Parse(Console.ReadLine() ?? string.Empty);

            Console.WriteLine("Enter product shopping: ");
            double shopping = double.Parse(Console.ReadLine() ?? string.Empty);

            shop.AddPhysicalProduct(name, price, weight, shopping);

        }

        // case 2: function to add a digital product
        public static void AddDigitalProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter product price: ");
            double price = double.Parse(Console.ReadLine() ?? string.Empty);

            Console.WriteLine("Enter file size in MB: ");
            double fileSizeMB = double.Parse(Console.ReadLine() ?? string.Empty);

            Console.WriteLine("Enter download link: ");
            string link = Console.ReadLine() ?? string.Empty;

            shop.AddDigitalProduct(name, price, fileSizeMB, link);
        }

        // case 3: function to register a customer
        public static void RegisterCustomer()
        {
            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter email: ");
            string email = Console.ReadLine() ?? string.Empty;

            shop.RegisterCustomer(fullName, email);
        }

        // case 4: function to place an order
        public static void PlaceOrder()
        {
            Console.Write("Enter customer email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter product ID: ");
            int productID = int.Parse(Console.ReadLine() ?? string.Empty);

            shop.PlaceOrder(email, productID);
        }

        // case 6: function to display a customer's order history
        public static void DisplayCustomerOrderHistory()
        {
            Console.Write("Enter customer email: ");
            string email = Console.ReadLine() ?? string.Empty;

            shop.DisplayCustomerOrders(email);
        }

        // case 7: function to cancel an order
        public static void CancelOrder()
        {
            Console.Write("Enter order ID to cancel: ");
            int orderID = int.Parse(Console.ReadLine() ?? string.Empty);

            shop.CancelOrder(orderID);
        }

        // case 8: function to display store statistics
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {

                DisplayMenu();
                int option = SelectOption();

                switch (option)
                {
                    case 1:

                        AddPhysicalProduct();

                        break;

                    case 2:

                        AddDigitalProduct();

                        break;

                    case 3:

                        RegisterCustomer();

                        break;

                    case 4:

                        PlaceOrder();

                        break;

                    case 5:

                        shop.DisplayAllProducts();

                        break;

                    case 6:

                        DisplayCustomerOrderHistory();

                        break;

                    case 7:

                        CancelOrder();

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
        public void SetPrice(double value)
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
        double weightKg;
        double shippingCostPerKg;

        // property to only get the weight of the physical product
        public double GetWeightKg { get; }

        // constructor to initialize the physical product
        public PhysicalProduct(string name, double price, double weightKg, double shippingCostPerKg) : base(name, price)
        {
            this.weightKg = weightKg;
            this.shippingCostPerKg = shippingCostPerKg;
        }

        // override method to display Physical Product
        public override void DisplayInfo()
        {
            Console.WriteLine("Physical product: ");
            Console.WriteLine("Physical Product ID: " + GetProductID + "\nName: " + name + "\nPrice: " + price + "\nWeight: " + weightKg + "kg" + "\nShipping Cost per kg : " + shippingCostPerKg);
        }

        // override method to calculate total cost of Physical Product
        public override double CalculateTotalCost()
        {
            price = price + (weightKg * shippingCostPerKg);
            return price;
        }

    }

    // DigitalProduct class inherits from Product, representing a digital product in the system
    class DigitalProduct : Product
    {
        double fileSizeMB;
        string downloadLink;

        // constructor to initialize the Digital product
        public DigitalProduct(string name, double price, double fileSizeMB, string downloadLink) : base(name, price)
        {
            this.fileSizeMB = fileSizeMB;
            this.downloadLink = downloadLink;
        }

        // override method to display Digital Product
        public override void DisplayInfo()
        {
            Console.WriteLine("Digital product: ");
            Console.WriteLine("Digital Product ID: " + GetProductID + "\nName: " + name + "\nPrice: " + price + "\nFile Size: " + fileSizeMB + "MB" + "\nDownload Link: " + downloadLink);
        }

    }

    // User class represents a user in the system, which can be a customer or an admin
    abstract class User
    {
        static int totalUsersCreated;
        protected string fullName;
        protected string email;

        // property to only get the full name of the user
        public string GetFullName { get; }

        // property to only get the email of the user
        public string GetEmail { get; }

        // constructor to initialize the user 
        public User(string fullName, string email)
        {
            this.fullName = fullName;
            this.email = email;
            totalUsersCreated++;
        }

        // property to only get the total number of users created
        public static int GetTotalUsersCreated()
        {
            return totalUsersCreated;
        }

        // Must be overridden (Each derived class must print its own relevant details.)
        public abstract void DisplayInfo();

    }

    // Customer class inherits from User, representing a customer in the system
    class Customer : User
    {
        List<Order> orders;

        // constructor to initialize the customer
        public Customer(string fullName, string email) : base(fullName, email)
        {
            orders = new List<Order>();
        }

        // override method to display Customer
        public override void DisplayInfo()
        {
            Console.WriteLine("Customer: ");
            Console.WriteLine("Full Name: " + fullName + "\nEmail: " + email + "number of orders placed: " + orders);
        }

        // method to add an order to the customer's order history
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        // method to remove an order from the customer's order history
        public void RemoveOrder(int orderID)
        {
            orders.RemoveAll(o => o.GetOrderID == orderID);
        }

        // method to display the customer's order history
        public void DisplayOrderHistory()
        {
            Console.WriteLine("Order History for " + fullName + ":");
            foreach (var order in orders)
            {
                if (order != null)
                {
                    order.DisplayInfo();
                    Console.WriteLine("-----------------------------------");
                }
                else
                {
                    Console.WriteLine("No orders yet!");
                }
            }
        }

    }

    // Admin class inherits from User, representing an administrator in the system
    class Admin : User
    {
        string role;

        public Admin(string fullName, string email, string role) : base(fullName, email)
        {
            this.role = role;
        }

        // override method to display Admin
        public sealed override void DisplayInfo()
        {
            Console.WriteLine("Admin: ");
            Console.WriteLine("Full Name: " + fullName + "\nEmail: " + email + "\nRole: " + role);
        }

    }

    class Order
    {
        static int nextOrderID = 5000; // Static field to generate unique order IDs
        int orderID;
        Customer customer;
        Product product;
        double totalCost;

        // property to only get the unique order ID
        public int GetOrderID { get; }

        // property to only get the total cost of the order
        public Customer GetCustomer { get; }

        // property to only get the total cost of the order
        public double GetTotalCost { get; }

        // constructor to initialize the order 
        public Order(Customer customer, Product product)
        {
            orderID = nextOrderID++;
            this.customer = customer;
            this.product = product;

            totalCost = product.CalculateTotalCost(); // Calculate total cost based on the product's price and any additional costs
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Order ID: " + orderID);
            Console.WriteLine("Customer: " + customer.GetFullName);
            Console.WriteLine("Product: " + product.GetName);
            Console.WriteLine("Total Cost paid: " + totalCost);
        }

    }

    class Store
    {
        private string StoreName { get; set; }
        List<Product> products;
        List<Customer> customers;
        List<Order> orders;

        public Store(string name)
        {
            StoreName = name;
            products = new List<Product>();
            customers = new List<Customer>();
            orders = new List<Order>();
        }

        //---------------------------------Product Methods----------------------------------    

        // method to add a physical product to the store's product list
        public void AddPhysicalProduct(string name, double price, double weight, double shippingPerKg)
        {
            PhysicalProduct product = new PhysicalProduct(name, price, weight, shippingPerKg);
            products.Add(product);
            Console.WriteLine("Physical product added successfully, with ID: " + product.GetProductID);
        }

        // method to add a digital product to the store's product list
        public void AddDigitalProduct(string name, double price, double fileSizeMB, string link)
        {
            DigitalProduct product = new DigitalProduct(name, price, fileSizeMB, link);

            products.Add(product);
            Console.WriteLine("Digital product added successfully, with ID: " + product.GetProductID);
        }

        public void DisplayAllProducts()
        {
            foreach (var product in products)
            {
                product.DisplayInfo();
                Console.WriteLine("-----------------------------------");
            }
        }

        //---------------------------------Customer Methods----------------------------------   

        // method to register a new customer, ensuring that the email is unique
        public void RegisterCustomer(string fullName, string email)
        {
            Customer info = customers.Find(c => c.GetEmail == email);

            if (info != null)
            {
                Console.WriteLine("This email already exists. Please enter a new email!");
                return;
            }

            Customer customer = new Customer(fullName, email);
            customers.Add(customer);
            Console.WriteLine("Customer registered successfully.");
        }

        // method to find a customer by their email address
        public Customer FindCustomer(string email)
        {
            foreach (var customer in customers)
            {
                if (customer.GetEmail == email)
                {
                    return customer;
                }
            }

            Console.WriteLine("Customer not found. Please enter a valid email.");
            return null;
        }

        //--------------------------------Order Methods----------------------------------

        public void PlaceOrder(string email, int productID)
        {
            Customer customer = customers.Find(c => c.GetEmail == email);
            if (customer == null)
            {
                Console.WriteLine("Order placement failed. Customer not found.");
                return;
            }

            Product product = products.Find(p => p.GetProductID == productID);
            if (product == null)
            {
                Console.WriteLine("Order placement failed. Product not found.");
                return;
            }

            // Create a new order and add it to the orders list and the customer's order history
            Order order = new Order(customer, product);
            orders.Add(order);
            customer.AddOrder(order);
            Console.WriteLine("Order placed successfully with Order ID: " + order.GetOrderID + " , and total cost: " + product.CalculateTotalCost);
        }

        //  method to cancel an existing order 
        public void CancelOrder(int orderID)
        {
            Order order = orders.Find(o => o.GetOrderID == orderID);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            customers.Remove(order.GetCustomer);
            orders.Remove(order);
            Console.WriteLine("Order cancelled successfully.");
        }

        public void DisplayCustomerOrders(string email)
        {
            Customer customer = customers.Find(c => c.GetEmail == email);
            if (customer == null)
            {
                Console.WriteLine("Customer not found. Please enter a valid email.");
                return;
            }

            customer.DisplayInfo();
            customer.DisplayOrderHistory();
        }

        //---------------------------------Statistics / Report----------------------------------

        public void DisplayStatistics()
        {
            double totalRevenue = orders.Sum(o => o.GetTotalCost);

            int PhysicalCount = 0;
            int DigitalCount = 0;

            foreach(var product in products)
            {
                if (product is PhysicalProduct)
                {
                    PhysicalCount++;
                }
                else if (product is DigitalProduct)
                {
                    DigitalCount++;
                }
            }

            Console.WriteLine("================== Shop Nest Report ===================");
            Console.WriteLine("Store name: " + StoreName);
            Console.WriteLine("Total Products: " + Product.GetTotalProductsCreated());
            Console.WriteLine("Physical count: " + PhysicalProduct.GetTotalProductsCreated);
            Console.WriteLine("Digital count: " + DigitalProduct.GetTotalProductsCreated());
            Console.WriteLine("Registered customers: " + User.GetTotalUsersCreated());
            Console.WriteLine("Total Orders: " + orders.Count);
            Console.WriteLine("Total revenue: " + totalRevenue);
            Console.WriteLine("Total users: " + User.GetTotalUsersCreated);
            Console.WriteLine("=======================================================");
        }

    }
}
