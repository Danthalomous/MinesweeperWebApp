document.addEventListener("DOMContentLoaded", function () {

    // Bind the event listeners to the game buttons
    document.querySelectorAll('.game-button').forEach(button => {
        button.addEventListener('click', handleButtonClick);
    });

    function handleButtonClick(event) {
        const row = parseInt(event.target.getAttribute('data-row'));
        const col = parseInt(event.target.getAttribute('data-col'));

        const cellState = getCellState(row, col);

        if (cellState === 'mine') {
            event.target.classList.add('bomb');
        } else if (cellState === 'empty') {
            event.target.classList.add('revealed');
            revealNeighbors(row, col);
        } else {
            event.target.classList.add('revealed');
            event.target.textContent = cellState; // Assuming cellState is a number here
        }

        checkGameOver();
    }

    function getCellState(row, col) {
        // Mock logic: For now, let's assume every third cell is a mine
        if (row % 3 === 0 && col % 3 === 0) {
            return 'mine';
        } else if (row % 2 === 0) { // Every even row will return a number (just for demonstration)
            return '2';
        }
        return 'empty';
    }

    function revealNeighbors(row, col) {
        // Mock logic: For demonstration purposes, we'll just reveal the immediate neighbors
        let neighbors = [
            { r: row - 1, c: col - 1 }, { r: row - 1, c: col }, { r: row - 1, c: col + 1 },
            { r: row, c: col - 1 }, { r: row, c: col + 1 },
            { r: row + 1, c: col - 1 }, { r: row + 1, c: col }, { r: row + 1, c: col + 1 }
        ];

        neighbors.forEach(neighbor => {
            let button = document.querySelector(`[data-row="${neighbor.r}"][data-col="${neighbor.c}"]`);
            if (button && !button.classList.contains('revealed')) {
                button.click();
            }
        });
    }

    function checkGameOver() {
        // Mock logic: For demonstration purposes, let's just alert "Game Over" if a bomb is clicked
        let bombs = document.querySelectorAll('.bomb');
        if (bombs.length > 0) {
            alert('Game Over!');
        }
    }

});
