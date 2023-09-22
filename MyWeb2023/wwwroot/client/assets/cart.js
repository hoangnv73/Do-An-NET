
bindingCart();
//setTotalOrder();
//function setTotalOrder() {
//    let arr = getCarts();
//    console.log("arr", arr.length);
//    document.getElementById("total-order-desktop").innerHTML = arr.length;
//}
function getCarts() {
    let productIds = getCookie("productIds");
    let arr = productIds != null ? JSON.parse(productIds) : [];
    return arr;
}
function AddToCart(productId) {
   //check null
    let arr = getCarts();
    let obj = {
        productId: productId,
        quantity: 1
    }
    arr.push(obj);
    var json_str = JSON.stringify(arr);
    document.cookie = "productIds=" + json_str;

    Swal.fire({
        icon: 'success',
        title: 'Successfully',
        text: 'Add to cart successfully!',
        //footer: '<a href="">Why do I have this issue?</a>'
    })
    //+1 UI cart
    document.getElementById("total-order-desktop").innerHTML = getCarts().length;
    bindingCart();
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}


function bindingCart() {
    let arr = getCarts();
    console.log("arr", arr);
    axios.post('/cart', {
        'request': arr
    }, {
        headers: {
            'content-type': 'application/x-www-form-urlencoded'
        }
    })
        .then(function (res) {

            var div = document.getElementById('render-cart');
            div.innerHTML = res.data;

            var div2 = document.getElementById('render-cart-2');
            div2.innerHTML = res.data;
        })
}

function RemoveToCart(productId) {
    let arr = getCarts();
    const newArr = arr.filter(x => {
        return x.productId !== productId;
    });
    var json_str = JSON.stringify(newArr);
    document.cookie = "productIds=" + json_str;

    document.getElementById("total-order-desktop").innerHTML = getCarts().length;
    bindingCart();
}    

function ResetCookie() {
    let arr = [];
    var json_str = JSON.stringify(arr);
    document.cookie = "productIds=" + json_str;
}

function Checkout() {
    let email = document.getElementById('email').value;
    let phone = document.getElementById('phone').value;
    let address = document.getElementById('address').value;
    let note = document.getElementById('note').value;
    let customerName = document.getElementById('customerName').value;
    let payment = document.getElementById('payment').value;
    let couponCode = document.getElementById('couponCode').value;
    let cartItems = getCarts();

    if (IsEmpty(email)) {
        ShowMessage("error", "Emai is required");
        return;
    }
    if (IsEmpty(phone)) {
        ShowMessage("error", "Phone is required");
        return;
    }
    if (IsEmpty(address)) {
        ShowMessage("error", "Address is required");
        return;
    }
    if (IsEmpty(customerName)) {
        ShowMessage("error", "Full name is required");
        return;
    }
    if (cartItems.length == 0) {
        ShowMessage("error", "Cart is empty");
        return;
    }

    axios.post('/checkout/index', {
        'email': email,
        'phone': phone,
        'address': address,
        'note': note,
        'customerName': customerName,
        'payment': payment,
        'cartItems': cartItems,
        'couponCode': couponCode, 
    }, {
        headers: {
            'content-type': 'application/x-www-form-urlencoded'
        }
    })
        .then(function (response) {
            //ShowMessage("success", "Order successfully!");
            //ResetCookie();
            //location.href = '/account/myorder'

            if (response.data.code == 400) {
                ShowMessage("error", response.data.message);
                return;
            }
            if (response.data.data == "cod") {
                location.href = '/account/myorder'
                
            }
            else {
                location.href = response.data;
            }
            ResetCookie();
        })
}

function IsEmpty(val) {
    return (val === undefined || val == null || val.length <= 0) ? true : false;
}

function ShowMessage(type, message) {
    Swal.fire({
        position: 'center',
        icon: type,
        title: message,
        showConfirmButton: false,
        timer: 2200
    })
}
