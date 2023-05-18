using Microsoft.AspNet.Identity;
using Nhom7_WebsiteClothes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom7_WebsiteClothes.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<ItemCart> ShoppingCart = GetShoppingCartFromSession();
            ViewBag.Tongsoluong = ShoppingCart.Sum(p => p.Quantity);
            ViewBag.Tongtien = ShoppingCart.Sum(p => p.Price * p.Quantity);
            return View(ShoppingCart);
        }

        public List<ItemCart> GetShoppingCartFromSession()
        {
            List<ItemCart> itemCart = Session["ShoppingCart"] as List<ItemCart>;
            if (itemCart == null)
            {
                itemCart = new List<ItemCart>();
                Session["ShoppingCart"] = itemCart;
            }
            return itemCart;
        }

        [Authorize]
        public ActionResult AddToCart(int clothesId, int quantity)
        {
            List<ItemCart> ShoppingCart = GetShoppingCartFromSession();

            ItemCart findItem = ShoppingCart.FirstOrDefault(p => p.Id == clothesId);

            if (findItem == null)
            {
                ClothesModelContext context = new ClothesModelContext();
                Cloth findBook = context.Clothes.FirstOrDefault(p => p.Id == clothesId);
                findItem = new ItemCart();
                findItem.Id = clothesId;
                findItem.Title = findBook.Title;
                findItem.Price = findBook.Price.Value;
                findItem.Image = findBook.Image;
                findItem.Quantity = quantity;
                ShoppingCart.Add(findItem);
            }
            else
            {
                findItem.Quantity+= quantity;
            }
            return RedirectToAction("Index");
        }
        public ActionResult SummaryCart()
        {
            List<ItemCart> ShoppingCart = GetShoppingCartFromSession();
            ViewBag.CartCount = ShoppingCart.Count;
            return PartialView();
        }

        public ActionResult RemoveCartItem(int clothesId)
        {
            List<ItemCart> ShoppingCart = GetShoppingCartFromSession();

            ItemCart findItem = ShoppingCart.FirstOrDefault(p => p.Id == clothesId);

            if (findItem != null)
            {
                ShoppingCart.Remove(findItem);
            }

            return RedirectToAction("Index");
        }
        public ActionResult ClearCart()
        {
            Session.Remove("ShoppingCart");
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCart(int clothesId, int txtQuantity)
        {
            List<ItemCart> ShoppingCart = GetShoppingCartFromSession();

            ItemCart findItem = ShoppingCart.FirstOrDefault(p => p.Id == clothesId);

            if (findItem != null)
            {
                findItem.Quantity = txtQuantity;
                findItem.Money = findItem.Price * findItem.Quantity;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Order()
        {
            string currentUserId = User.Identity.GetUserId();
            ClothesModelContext context = new ClothesModelContext();
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order objOrder = new Order()
                    {
                        CustomerId = null,
                        OrderDate = DateTime.Now,
                        DeliveryDate = null,
                        isPaid = false,
                        isComplete = false

                    };
                    objOrder = context.Orders.Add(objOrder);
                    context.SaveChanges();

                    List<ItemCart> ShoppingCart = GetShoppingCartFromSession();
                    foreach (var item in ShoppingCart)
                    {
                        OrderDetail ctdh = new OrderDetail()
                        {
                            OrderNo = objOrder.OrderNo,
                            ClothesId = item.Id,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        context.OrderDetails.Add(ctdh);
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content("Gặp lỗi khi đặt hàng:" + ex.Message);
                }
            }
            Session["ShoppingCart"] = null;
            return RedirectToAction("ConfirmOrder", "ShoppingCart");
        }
        public ActionResult ConfirmOrder()
        {
            return View();
        }
    }
}