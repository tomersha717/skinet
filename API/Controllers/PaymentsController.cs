using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{

    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private const string WhSecret = "whsec_1ff556cce4a987f8e9960b7fd5427511c4992855f0e656d0af87095b616d862b";
        private readonly ILogger<IPaymentService> _logger;
        public PaymentsController(IPaymentService paymentService, ILogger<IPaymentService> logger)
        {
            _logger = logger;
            _paymentService = paymentService;
        }


        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket =  await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            
            if (basket == null) return BadRequest(new ApiResponse(400,"Problem with your basket"));
            
            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret, 300, false);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded");
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment failed: ", order.Id);
                    break;
            }

            return new EmptyResult();
        }




    }
}