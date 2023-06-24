function showMessageError(message) {
    console.log('ok' + message)
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: message,
    })
}

function paySuccess() {
    Swal.fire({
        position: 'center',
        icon: 'success',
        title: 'Payment success!',
        showConfirmButton: false,
        timer: 2200
    })
}