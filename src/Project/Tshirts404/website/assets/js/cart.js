const productsCart = "e-commerce--products-cart";
const cartInvoice = "e-commerce--invoices-cart";
const form = document.getElementById("product-form");
const cartBadge = document.querySelector(".cart__badge");
const detailCartTable = document.querySelector(".detail-cart-table");
const subTotalData = document.querySelector(".sub-total__data");
const shippingData = document.querySelector(".shipping__data");
const totalPurchase = document.querySelector(".total__purchase");
const orderDetailsWrap = document.querySelector(".order-details-wrap");
const orderDetailsBody = document.querySelector(".order-details-body");
const placeOrderBtn = document.querySelector(".place-order__btn");
const checkoutSection = document.querySelector(".checkout-section");
const checkoutDetailsSubtotal = document.querySelector(
  ".checkout-details__subtotal"
);
const checkoutDetailsShipping = document.querySelector(
  ".checkout-details__shipping"
);
const checkoutDetailsTotal = document.querySelector(".checkout-details__total");
const cart = JSON.parse(localStorage.getItem(productsCart)) || [];
const cartTotals = JSON.parse(localStorage.getItem(cartInvoice)) || {};

const logSubmit = () => {
  const product = [...form.elements]
    .filter((el) => el.tagName !== "BUTTON")
    .reduce((acc, item) => ({ ...acc, [item.name]: item.value }), {});
  const verifyProductInCart = cart.find(
    (productCart) => productCart.productId === product.productId
  );
  if (verifyProductInCart) return alert("Product is already in cart");
  cart.push(product);
  localStorage.setItem(productsCart, JSON.stringify(cart));
};

const displayCartItems = () => {
  if (cart.length < 1) {
    cartBadge.style.display = "none";
    return;
  }
  cartBadge.style.display = "block";
  cartBadge.innerHTML = cart.length;
};

const deleteProduct = (event, productId) => {
  event.currentTarget.closest(".table-body-row").remove();
  var filteredArray = cart.filter((e) => parseInt(e.productId) !== productId);
  localStorage.setItem(productsCart, JSON.stringify(filteredArray));
};

const updateTotal = () => {
  let subTotal = 0;
  const totalProducts = detailCartTable.querySelectorAll(".product-total");
  totalProducts.forEach((totalProduct) => {
    subTotal += parseFloat(totalProduct.innerHTML.replace("$", ""));
  });

  const shippingCost = parseFloat(shippingData.innerHTML);
  const total = subTotal + shippingCost;

  subTotalData.innerHTML = subTotal;
  totalPurchase.innerHTML = total;

  localStorage.setItem(
    cartInvoice,
    JSON.stringify({
      subTotal: subTotalData.innerHTML,
      shipping: shippingCost,
      total,
    })
  );
};

const changeQuantityProduct = (event, productId) => {
  const productRow = event.currentTarget.closest(".table-body-row");
  const productPrice = productRow.querySelector(".product-price");
  const productTotal = productRow.querySelector(".product-total");
  productTotal.innerHTML = `$${
    parseFloat(event.target.value) * parseFloat(productPrice.innerHTML)
  }`;

  const productIndex = cart.findIndex(
    (product) => product.productId == productId
  );
  cart[productIndex].quantity = event.target.value;
  localStorage.setItem(productsCart, JSON.stringify(cart));
  updateTotal();
};

const cartDetailRow = ({
  productId,
  color,
  productImage,
  productPrice,
  quantity,
  size,
  productStock,
  productName,
}) => {
  return `<tr class="table-body-row">
    <td class="product-remove"><a href="#" onClick="deleteProduct(event, ${productId})"><i class="far fa-window-close"></i></a></td>
    <td class="product-image"><img src="${productImage}" alt="">
    </td>
    <td class="product-name">${productName} <span>(${color})</span> <span>(${size})</span></td>
    <td class="product-price">${productPrice}</td>
    <td class="product-quantity"><input type="number" onChange="changeQuantityProduct(event, ${productId})" placeholder="0" value="${quantity}" min="1" max="${productStock}"></td>
    <td class="product-total">$${quantity * productPrice}</td>
</tr>`;
};

const orderDetailsRow = ({ productName, productPrice, quantity }) => {
  return `<tr>
    <td>${productName}</td>
    <td>$${parseFloat(productPrice) * parseFloat(quantity)}</td>
</tr>`;
};

const addProductDetailRow = () => {
  if (detailCartTable === null) return;
  cart.forEach(
    (element) => (detailCartTable.innerHTML += cartDetailRow(element))
  );
};

const setOrderDetailsWrap = () => {
  checkoutDetailsSubtotal.innerHTML = cartTotals.subTotal;
  checkoutDetailsShipping.innerHTML = cartTotals.shipping;
  checkoutDetailsTotal.innerHTML = cartTotals.total;
  cart.forEach(
    (element) => (orderDetailsBody.innerHTML += orderDetailsRow(element))
  );
};

const getOrderJson = () => {
  return {
    ID: "DB_TEST000002-003",
    FromUser: {
      ID: "MiddlewareIntegrationsUser",
      Username: "Default_Admin",
      Password: null,
      FirstName: "Default",
      LastName: "User",
      Email: "test@test.com",
      Phone: null,
      TermsAccepted: null,
      Active: true,
      xp: {},
      AvailableRoles: null,
      Locale: null,
      DateCreated: "2021-12-27T17:10:58.103+00:00",
      PasswordLastSetDate: null,
    },
    FromCompanyID: "AgrSsER6alaUYx87",
    ToCompanyID: "003",
    FromUserID: "MiddlewareIntegrationsUser",
    BillingAddressID: null,
    BillingAddress: null,
    ShippingAddressID: "0001-default-buyerLocation",
    Comments: null,
    LineItemCount: 1,
    Status: "Completed",
    DateCreated: "2022-02-07T19:02:26.35+00:00",
    DateSubmitted: "2022-02-07T19:02:26.397+00:00",
    DateApproved: null,
    DateDeclined: null,
    DateCanceled: null,
    DateCompleted: "2022-02-07T19:10:09.087+00:00",
    LastUpdated: "2022-02-07T19:10:09.14+00:00",
    Subtotal: 45,
    ShippingCost: 0,
    TaxCost: 0,
    PromotionDiscount: 0,
    Currency: null,
    Total: 45,
    IsSubmitted: true,
    LineItems: null,
    xp: {
      ExternalTaxTransactionID: null,
      ShipFromAddressIDs: ["003-01"],
      SupplierIDs: ["003"],
      NeedsAttention: false,
      StopShipSync: false,
      OrderType: "Standard",
      QuoteOrderInfo: null,
      Returns: null,
      Cancelations: null,
      Currency: "USD",
      SubmittedOrderStatus: "Completed",
      ApprovalNeeded: null,
      ShippingStatus: "Shipped",
      ClaimStatus: "NoClaim",
      PaymentMethod: null,
      ShippingAddress: {
        xp: null,
        ID: "0001-default-buyerLocation",
        CompanyName: null,
        FirstName: null,
        LastName: null,
        Street1: "110 5th St",
        Street2: null,
        City: "Minneaplis",
        State: "Minnesota",
        Zip: "55403",
        Country: "US",
        Phone: null,
        AddressName: null,
      },
      SelectedShipMethodsSupplierView: [
        {
          EstimatedTransitDays: 3,
          Name: "FREE",
          ShipFromAddressID: "003-01",
        },
      ],
      IsResubmitting: null,
      HasSellerProducts: null,
      InvoiceNumber: "WERW234",
    },
  };
};
const sendOrder = () => {
  console.log([...document.querySelectorAll(".order__field")]);

  (async () => {
    const rawResponse = await fetch(checkoutSection.dataset.endpoint, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(getOrderJson()),
    });
    const content = await rawResponse.json();

    console.log(content);
  })();
};

if (detailCartTable) {
  addProductDetailRow();
  updateTotal();
}

if (orderDetailsWrap) {
  setOrderDetailsWrap();
}

displayCartItems();

form?.addEventListener("submit", logSubmit);
placeOrderBtn.addEventListener("click", sendOrder);
