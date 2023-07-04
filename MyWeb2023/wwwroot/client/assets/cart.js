bindingCart();

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
        position: 'center',
        icon: 'success',
        title: 'Add to cart successfully!',
        showConfirmButton: false,
        timer: 2200
    })
    bindingCart();
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}


function bindingCart() {
    let arr = getCarts();
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
        })
}

function RemoveToCart(productId) {
    let arr = getCarts();
    const newArr = arr.filter(x => {
        return x.productId !== productId;
    });
    var json_str = JSON.stringify(newArr);
    document.cookie = "productIds=" + json_str;
    bindingCart();
}    

            