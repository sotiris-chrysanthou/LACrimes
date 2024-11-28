window.setDateInputFormat = (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        element.addEventListener('input', (event) => {
            const value = event.target.value;
            const formattedValue = value.split('-').reverse().join('-');
            event.target.value = formattedValue;
        });
    }
};