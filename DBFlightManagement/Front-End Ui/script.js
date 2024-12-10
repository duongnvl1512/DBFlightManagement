// script.js
function openModal(type) {
    const modal = document.getElementById(`${type}Modal`);
    modal.style.display = 'block';
    document.body.style.overflow = 'hidden';
}

function closeModal(type) {
    const modal = document.getElementById(`${type}Modal`);
    modal.style.display = 'none';
    document.body.style.overflow = 'auto';
}

// Đóng modal khi click bên ngoài
window.onclick = function (event) {
    const modals = document.getElementsByClassName('modal');
    for (let modal of modals) {
        if (event.target == modal) {
            modal.style.display = 'none';
            document.body.style.overflow = 'auto';
        }
    }
}

// Xử lý form submission
document.addEventListener('DOMContentLoaded', function () {
    // Ngăn chặn form submission mặc định và xử lý dữ liệu
    const forms = document.getElementsByTagName('form');
    for (let form of forms) {
        form.addEventListener('submit', function (e) {
            e.preventDefault();
            handleFormSubmit(e.target);
        });
    }
});

// Xử lý dữ liệu form submit
function handleFormSubmit(form) {
    const formData = new FormData(form);
    const formObject = {};
    formData.forEach((value, key) => {
        formObject[key] = value;
    });

    // Xác định loại form dựa trên parent modal
    const modalType = form.closest('.modal').id;

    switch (modalType) {
        case 'addModal':
            handleAddFlight(formObject);
            break;
        case 'deleteModal':
            handleDeleteFlight(formObject);
            break;
        case 'editModal':
            handleEditFlight(formObject);
            break;
        case 'updateModal':
            handleUpdateFlight(formObject);
            break;
    }
}

// Xử lý thêm chuyến bay
function handleAddFlight(data) {
    console.log('Thêm chuyến bay mới:', data);
    // Thực hiện API call để thêm chuyến bay
    // Ví dụ:
    // await fetch('/api/flights', {
    //     method: 'POST',
    //     headers: {
    //         'Content-Type': 'application/json',
    //     },
    //     body: JSON.stringify(data)
    // });

    showNotification('Thêm chuyến bay thành công!', 'success');
    closeModal('add');
}

// Xử lý xóa chuyến bay
function handleDeleteFlight(data) {
    console.log('Xóa chuyến bay:', data);
    // Thực hiện API call để xóa chuyến bay
    // Ví dụ:
    // await fetch(`/api/flights/${data.deleteFlightNumber}`, {
    //     method: 'DELETE'
    // });

    showNotification('Xóa chuyến bay thành công!', 'success');
    closeModal('delete');
}

// Xử lý sửa chuyến bay
function handleEditFlight(data) {
    console.log('Sửa chuyến bay:', data);
    // Thực hiện API call để sửa chuyến bay
    // Ví dụ:
    // await fetch(`/api/flights/${data.editFlightNumber}`, {
    //     method: 'PUT',
    //     headers: {
    //         'Content-Type': 'application/json',
    //     },
    //     body: JSON.stringify(data)
    // });

    showNotification('Cập nhật thông tin chuyến bay thành công!', 'success');
    closeModal('edit');
}

// Xử lý cập nhật trạng thái chuyến bay
function handleUpdateFlight(data) {
    console.log('Cập nhật trạng thái chuyến bay:', data);
    // Thực hiện API call để cập nhật trạng thái
    // Ví dụ:
    // await fetch(`/api/flights/${data.updateFlightNumber}/status`, {
    //     method: 'PATCH',
    //     headers: {
    //         'Content-Type': 'application/json',
    //     },
    //     body: JSON.stringify({ status: data.flightStatus, note: data.statusNote })
    // });

    showNotification('Cập nhật trạng thái chuyến bay thành công!', 'success');
    closeModal('update');
}

// Hiển thị thông báo
function showNotification(message, type = 'success') {
    // Tạo element thông báo
    const notification = document.createElement('div');
    notification.className = `notification ${type}`;
    notification.textContent = message;

    // Thêm style cho notification
    notification.style.position = 'fixed';
    notification.style.top = '20px';
    notification.style.right = '20px';
    notification.style.padding = '15px 25px';
    notification.style.borderRadius = '5px';
    notification.style.animation = 'slideIn 0.5s ease';
    notification.style.zIndex = '2000';

    if (type === 'success') {
        notification.style.backgroundColor = '#4CAF50';
        notification.style.color = 'white';
    } else if (type === 'error') {
        notification.style.backgroundColor = '#f44336';
        notification.style.color = 'white';
    }

    // Thêm notification vào body
    document.body.appendChild(notification);

    // Tự động xóa notification sau 3 giây
    setTimeout(() => {
        notification.style.animation = 'slideOut 0.5s ease';
        setTimeout(() => {
            notification.remove();
        }, 500);
    }, 3000);
}

// Thêm style cho animation của notification
const style = document.createElement('style');
style.textContent = `
    @keyframes slideIn {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }

    @keyframes slideOut {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(100%);
            opacity: 0;
        }
    }
`;
document.head.appendChild(style);

// Validation functions
function validateFlightNumber(flightNumber) {
    return /^[A-Z0-9]{2,6}$/.test(flightNumber);
}

function validateDate(date) {
    const selectedDate = new Date(date);
    const today = new Date();
    return selectedDate >= today;
}

function validatePrice(price) {
    return price > 0;
}

// Add form validation
const addForm = document.querySelector('#addModal form');
if (addForm) {
    addForm.addEventListener('input', function (e) {
        const input = e.target;
        const value = input.value;

        switch (input.id) {
            case 'flightNumber':
                if (!validateFlightNumber(value)) {
                    input.setCustomValidity('Mã chuyến bay phải từ 2-6 ký tự, chỉ gồm chữ hoa và số');
                } else {
                    input.setCustomValidity('');
                }
                break;
            case 'departureDate':
                if (!validateDate(value)) {
                    input.setCustomValidity('Ngày khởi hành phải từ ngày hiện tại trở đi');
                } else {
                    input.setCustomValidity('');
                }
                break;
            case 'price':
                if (!validatePrice(value)) {
                    input.setCustomValidity('Giá vé phải lớn hơn 0');
                } else {
                    input.setCustomValidity('');
                }
                break;
        }
    });
}