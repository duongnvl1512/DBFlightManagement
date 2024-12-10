// flight-data.js

// Khởi tạo mảng lưu trữ dữ liệu chuyến bay
let flightData = [
    {
        id: 'VJ001',
        airline: 'Vietjet',
        departure: 'HAN',
        destination: 'SGN',
        departureTime: '12:00',
        arrivalTime: '18:00',
        price: 3000000,
        logo: './vietjet.png',
        status: 'active'
    },
    {
        id: 'JS001',
        airline: 'JetStar',
        departure: 'DANANG',
        destination: 'HOIAN',
        departureTime: '14:00',
        arrivalTime: '20:00',
        price: 3200000,
        logo: './jetstar.png',
        status: 'active'
    },
    {
        id: 'VNA001',
        airline: 'Vietnam Airlines',
        departure: 'DALAT',
        destination: 'VUNGTAU',
        departureTime: '08:00',
        arrivalTime: '14:00',
        price: 2000000,
        logo: './Vietnamairline.png',
        status: 'active'
    }
];

// Hàm render flight card
function createFlightCard(flight) {
    return `
        <div class="flight-card" data-flight-id="${flight.id}">
            <div class="airline-info">
                <div class="airline-details">
                    <img class="airline-logo" src="${flight.logo}" alt="${flight.airline}">
                </div>
                <div class="flight-details">
                    <div class="flight-times">
                        <span class="time">${flight.departureTime}</span>
                        <span class="time"> - </span>
                        <span class="time">${flight.arrivalTime}</span>
                    </div>
                    <div class="route-line">
                        <span class="dot"></span>
                        <span class="line"></span>
                        <span class="dot"></span>
                    </div>
                    <div class="airports">
                        <span>${flight.departure}</span>
                        <span>${flight.destination}</span>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="price">
                    <span class="currency">đ</span>
                    <span class="amount">${flight.price.toLocaleString()}</span>
                </div>
                <button class="btn-secondary">Xem chuyến bay</button>
            </div>
        </div>
    `;
}

// Hàm cập nhật giao diện tìm kiếm
function updateFlightResults(flights = flightData) {
    const flightCardsContainer = document.querySelector('.flight-cards');
    const activeFlights = flights.filter(flight => flight.status === 'active');

    // Cập nhật số lượng chuyến bay
    const resultsHeader = document.querySelector('.results-header p');
    resultsHeader.innerHTML = `Showing <strong>${activeFlights.length}</strong> of <strong>${flightData.length}</strong> flights`;

    // Render các flight cards
    flightCardsContainer.innerHTML = activeFlights
        .map(flight => createFlightCard(flight))
        .join('');
}

// Sửa lại các hàm xử lý CRUD để cập nhật dữ liệu và giao diện
function handleAddFlight(data) {
    const newFlight = {
        id: data.flightNumber,
        airline: getAirlineFromFlightNumber(data.flightNumber),
        departure: data.departure,
        destination: data.destination,
        departureTime: data.departureTime,
        arrivalTime: calculateArrivalTime(data.departureTime),
        price: parseInt(data.price),
        logo: getAirlineLogo(getAirlineFromFlightNumber(data.flightNumber)),
        status: 'active'
    };

    flightData.push(newFlight);
    updateFlightResults();
    showNotification('Thêm chuyến bay thành công!', 'success');
    closeModal('add');
}

function handleDeleteFlight(data) {
    const index = flightData.findIndex(flight => flight.id === data.deleteFlightNumber);
    if (index !== -1) {
        flightData[index].status = 'deleted';
        updateFlightResults();
        showNotification('Xóa chuyến bay thành công!', 'success');
    } else {
        showNotification('Không tìm thấy chuyến bay!', 'error');
    }
    closeModal('delete');
}

function handleEditFlight(data) {
    const index = flightData.findIndex(flight => flight.id === data.editFlightNumber);
    if (index !== -1) {
        flightData[index] = {
            ...flightData[index],
            departure: data.editDeparture,
            destination: data.editDestination,
            departureTime: data.editDepartureTime,
            arrivalTime: calculateArrivalTime(data.editDepartureTime),
            price: parseInt(data.editPrice)
        };
        updateFlightResults();
        showNotification('Cập nhật thông tin chuyến bay thành công!', 'success');
    } else {
        showNotification('Không tìm thấy chuyến bay!', 'error');
    }
    closeModal('edit');
}

function handleUpdateFlight(data) {
    const index = flightData.findIndex(flight => flight.id === data.updateFlightNumber);
    if (index !== -1) {
        flightData[index].status = data.flightStatus === 'cancelled' ? 'deleted' : 'active';
        updateFlightResults();
        showNotification('Cập nhật trạng thái chuyến bay thành công!', 'success');
    } else {
        showNotification('Không tìm thấy chuyến bay!', 'error');
    }
    closeModal('update');
}

// Utility functions
function getAirlineFromFlightNumber(flightNumber) {
    const prefix = flightNumber.substring(0, 2);
    const airlines = {
        'VJ': 'Vietjet',
        'JS': 'JetStar',
        'VN': 'Vietnam Airlines',
        'QT': 'Qatar Airways'
    };
    return airlines[prefix] || 'Unknown Airline';
}

function getAirlineLogo(airline) {
    const logos = {
        'Vietjet': './vietjet.png',
        'JetStar': './jetstar.png',
        'Vietnam Airlines': './Vietnamairline.png',
        'Qatar Airways': './qatar.png'
    };
    return logos[airline] || './default-airline.png';
}

function calculateArrivalTime(departureTime) {
    // Giả lập tính thời gian đến (thêm 6 tiếng)
    const [hours, minutes] = departureTime.split(':');
    const arrivalHours = (parseInt(hours) + 6) % 24;
    return `${arrivalHours.toString().padStart(2, '0')}:${minutes}`;
}

// Initialize the flight results when page loads
document.addEventListener('DOMContentLoaded', () => {
    updateFlightResults();

    // Xử lý sắp xếp
    const sortSelect = document.querySelector('.sort-select');
    sortSelect.addEventListener('change', (e) => {
        let sortedFlights = [...flightData];
        switch (e.target.value) {
            case 'Sort by Giá':
                sortedFlights.sort((a, b) => a.price - b.price);
                break;
            case 'Sort by Thời lượng':
                sortedFlights.sort((a, b) => a.departureTime.localeCompare(b.departureTime));
                break;
            case 'Sort by Điểm đến':
                sortedFlights.sort((a, b) => a.destination.localeCompare(b.destination));
                break;
        }
        updateFlightResults(sortedFlights);
    });

    // Xử lý filter
    const priceRange = document.getElementById('priceRange');
    priceRange.addEventListener('input', (e) => {
        const maxPrice = e.target.value * 10000; // Convert to VND
        const filteredFlights = flightData.filter(flight => flight.price <= maxPrice);
        updateFlightResults(filteredFlights);
    });

    // Xử lý filter thời gian
    const timeRange = document.getElementById('timeRange');
    timeRange.addEventListener('input', (e) => {
        const maxHour = e.target.value;
        const filteredFlights = flightData.filter(flight => {
            const hour = parseInt(flight.departureTime.split(':')[0]);
            return hour <= maxHour;
        });
        updateFlightResults(filteredFlights);
    });

    // Xử lý filter hãng bay
    const airlineCheckboxes = document.querySelectorAll('.airline-options input');
    airlineCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', () => {
            const selectedAirlines = Array.from(airlineCheckboxes)
                .filter(cb => cb.checked)
                .map(cb => cb.parentElement.textContent.trim());

            if (selectedAirlines.length === 0) {
                updateFlightResults(); // Show all if none selected
            } else {
                const filteredFlights = flightData.filter(flight =>
                    selectedAirlines.includes(flight.airline)
                );
                updateFlightResults(filteredFlights);
            }
        });
    });
});