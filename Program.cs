using System;
using System.Collections.Generic;

namespace RestaurantMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Restaurant Menu!");

            List<MenuItem> menu = new List<MenuItem>();
            menu.Add(new MenuItem("Burger", 10.99));
            menu.Add(new MenuItem("Pizza", 15.99));
            menu.Add(new MenuItem("Pasta", 12.99));
            menu.Add(new MenuItem("Sandwich", 8.99));

            Order order = new Order();

            while (true)
            {
                Console.WriteLine("\nMenu:");
                foreach (MenuItem item in menu)
                {
                    Console.WriteLine($"{item.Name} - ${item.Price}");
                }

                Console.Write("\nEnter the item name to add to the order (or type 'done' to finish order): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "done")
                {
                    break;
                }
                else
                {
                    MenuItem item = menu.Find(i => i.Name.ToLower() == input.ToLower());
                    if (item != null)
                    {
                        order.AddItem(item);
                        Console.WriteLine($"{item.Name} added to the order.");
                    }
                    else
                    {
                        Console.WriteLine($"Sorry, {input} is not on the menu.");
                    }
                }
            }

            Console.WriteLine("\nOrder Summary:");
            foreach (OrderItem item in order.Items)
            {
                Console.WriteLine($"{item.MenuItem.Name} - ${item.MenuItem.Price} x {item.Quantity} = ${item.GetTotal():0.00}");
            }
            Console.WriteLine($"Total Bill: ${order.GetTotal():0.00}");
        }
    }

    class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public MenuItem(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    class Order
    {
        public List<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }

        public void AddItem(MenuItem item)
        {
            OrderItem existingItem = Items.Find(i => i.MenuItem.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Items.Add(new OrderItem(item));
            }
        }

        public double GetTotal()
        {
            double total = 0;
            foreach (OrderItem item in Items)
            {
                total += item.GetTotal();
            }
            return total;
        }
    }

    class OrderItem
    {
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }

        public OrderItem(MenuItem item)
        {
            MenuItem = item;
            Quantity = 1;
        }

        public double GetTotal()
        {
            return MenuItem.Price * Quantity;
        }
    }
}

