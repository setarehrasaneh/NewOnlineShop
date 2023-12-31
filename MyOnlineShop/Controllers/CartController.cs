﻿using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Domain.Entities;
using MyOnlineShop.Domain.Helpers;
using MyOnlineShop.Domain.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOnlineShop.Controllers
{
    public class CartController : Controller
    {

        private readonly IOrderService _orderService;

        public CartController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {

            var cart = await Task.Run(() => SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart"));
            ViewBag.cart = cart;
            ViewBag.total =  _orderService.GetTotalPrice(cart);
            return View();
        }

        public async Task<IActionResult> AddToCart(int id)
        {

            List<OrderItem> cart = await Task.Run(() => SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart"));
            var newCart = await _orderService.AddItemsToOrder(cart, id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", newCart);
            return RedirectToAction("Index");
 
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            List<OrderItem> cart = await Task.Run(()=> SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart"));
            var newCart = _orderService.RemoveItemFromOrder(cart, id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", newCart);
            return RedirectToAction("Index");

        }
    }
}
