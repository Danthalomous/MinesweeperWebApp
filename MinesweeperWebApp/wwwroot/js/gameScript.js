document.addEventListener("DOMContentLoaded", function () {
    console.log("gameScript.js Loaded");
    // Bind the event listeners to the game buttons
    document.querySelectorAll('.game-button').forEach(button => {
        button.addEventListener('click', function (event) {
            handleButtonClick(event, "/game/ShowOneCell");
        });
    });

    /*
    $(document).on("click", ".game-button") {
        function("handleButtonClick") {

        }
   
*/
    function handleButtonClick(event, urlString) {
        var row = event.target.getAttribute('data-row');
        var col = event.target.getAttribute('data-col');
        var val = $(this).val;
        val = parseInt(val);

        console.log("The button clicked's [r,c]: " + row + "," + col) + "\n" + val;

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

        $.ajax({
            type: "json",
            method: "POST",
            url: urlString,
            data: {
                "buttonNumber": val
            },
            success: function (data) {
                console.log(data);
                $("#" + val).html(data);
            }
        });
    }

    function getCellState(row, col) {
        row = parseInt(row);
        col = parseInt(col);
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