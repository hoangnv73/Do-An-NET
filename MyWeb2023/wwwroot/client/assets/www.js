function showMessageError(message) {
    console.log('ok' + message)
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: message,
    })
}

function showMessageSuccess(message) {
    console.log('ok' + message)
    Swal.fire({
        icon: 'success',
        title: 'Success',
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
function Success() {
    Swal.fire({
  icon: 'success',
  title: 'Success',
  text: 'Change Password success!',
})
}