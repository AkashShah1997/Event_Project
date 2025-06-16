document.addEventListener('DOMContentLoaded', () => {
    const API_BASE_URL = 'http://localhost:5000/api/events'; // Adjust port if necessary
    const eventsTableBody = document.getElementById('events-table-body');
    const daysFilter = document.getElementById('days-filter');
    const errorDisplay = document.getElementById('error-message');
    let currentEvents = [];
    let sortDirection = { name: 'asc', startDate: 'asc' };

    const fetchEvents = async (days) => {
        try {
            const response = await fetch(`${API_BASE_URL}?days=${days}`);
            if (!response.ok) {
                throw new Error(`API request failed with status ${response.status}`);
            }
            currentEvents = await response.json();
            renderTable(currentEvents);
            errorDisplay.style.display = 'none';
        } catch (error) {
            // Implement error handling for API failures 
            console.error('Failed to fetch events:', error);
            errorDisplay.textContent = 'Could not load event data. Please try again later.';
            errorDisplay.style.display = 'block';
            eventsTableBody.innerHTML = '<tr><td colspan="3">Error loading data.</td></tr>';
        }
    };

    const renderTable = (events) => {
        eventsTableBody.innerHTML = '';
        if (events.length === 0) {
            eventsTableBody.innerHTML = '<tr><td colspan="3">No upcoming events found.</td></tr>';
            return;
        }
        events.forEach(event => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${event.name}</td>
                <td>${new Date(event.startDate).toLocaleString()}</td>
                <td>${new Date(event.endDate).toLocaleString()}</td>
            `;
            eventsTableBody.appendChild(row);
        });
    };

    const sortEvents = (key) => {
        const direction = sortDirection[key] === 'asc' ? 1 : -1;
        currentEvents.sort((a, b) => {
            if (a[key] < b[key]) return -1 * direction;
            if (a[key] > b[key]) return 1 * direction;
            return 0;
        });
        sortDirection[key] = sortDirection[key] === 'asc' ? 'desc' : 'asc';
        renderTable(currentEvents);
    };

    // Event Listeners
    daysFilter.addEventListener('change', (e) => fetchEvents(e.target.value));
    document.querySelectorAll('th[data-sort]').forEach(th => {
        th.addEventListener('click', () => sortEvents(th.dataset.sort));
    });

    // Initial load
    fetchEvents(daysFilter.value);
});