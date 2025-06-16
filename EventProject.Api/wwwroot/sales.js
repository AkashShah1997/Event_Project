document.addEventListener('DOMContentLoaded', () => {
    const API_BASE_URL = 'http://localhost:5000/api/events/sales-summary'; // Adjust port if necessary

    const fetchAndRenderSummary = async (endpoint, tableId, valueFormatter) => {
        const tableBody = document.getElementById(tableId);
        try {
            const response = await fetch(`${API_BASE_URL}/${endpoint}`);
            if (!response.ok) throw new Error('API Error');
            const data = await response.json();

            tableBody.innerHTML = '';
            data.forEach(item => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${item.eventName}</td><td>${valueFormatter(item.salesValue)}</td>`;
                tableBody.appendChild(row);
            });
        } catch (error) {
            console.error(`Failed to fetch ${endpoint}:`, error);
            tableBody.innerHTML = `<tr><td colspan="2">Error loading data.</td></tr>`;
        }
    };

    // Fetch both summaries
    fetchAndRenderSummary('by-count', 'sales-by-count', value => value);
    fetchAndRenderSummary('by-value', 'sales-by-value', value => `$${value.toLocaleString()}`);
});