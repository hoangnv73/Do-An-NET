function showMessageError(message) {
    console.log('ok' + message)
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: message,
    })
}

function CommonMessage(type, message) {
    Swal.fire({
        position: 'center',
        icon: type,
        title: message,
        showConfirmButton: false,
        timer: 2200
    })
}