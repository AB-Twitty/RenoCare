$('document').ready(() => {
    function removeTimeSlot(event) {
        // Get the parent div.time-slot data-id value
        var dataId = $(event.target).closest('.time-slot').data('id');
        // In the div#session-inputs search for child div with data-idx == dataId
        var matchingDiv = $('#session-inputs').find('div[data-idx="' + dataId + '"]');
        if (matchingDiv.length > 0) {
            // Find the input field inside the matchingDiv
            var input = matchingDiv.find(`input[name="Sessions[${dataId}].Deleted"]`);
            input.val(true);
        }

        // Remove the parent div.time-slot
        $(event.target).closest('.time-slot').remove();
    }


    $('.time-slot button').on('click', (e) => {
        removeTimeSlot(e);
    });

    $('.clock-input').each(function () {
        var input = $(this); // This creates a closure around 'input'
        input.clockpicker({
            twelvehour: true,
            placement: 'bottom',
            align: 'right',
            autoclose: false,
            default: 'now',
            donetext: "Add",
            afterDone: function () {
                let inputs = $('#session-inputs');
                let index = parseInt(inputs.attr('data-cnt'), 10); // Get data-cnt value and parse it as an integer

                // Check if index is not a number and return early
                if (isNaN(index) || index < 0) {
                    console.error('Failed to parse data-cnt as an integer');
                    return; 
                }

                if (inputs.find('div[data-idx="' + index + '"]').length > 0) {
                    console.error('Index already in use.');
                    return; 
                }

                let day = input.closest('.ibox').data('day'); 

                let time = input.val().replace(/(AM|PM)$/i, ' $1');


                let input_time = convertTo24Hour(time);

                inputs.append(
                    `
                    <div data-idx="${index}">
                        <input hidden name="Sessions[${index}].Day" value="${day}" />
                        <input hidden name="Sessions[${index}].Time" value="${input_time}" />
                        <input name="Sessions[${index}].Deleted" value="false" />
                    </div>
                    `
                );

                inputs.attr('data-cnt', index + 1);

                let content_elem = input.closest('.clockpicker-header').next('.clockpicker-content');

                addTimeSlot(content_elem, time, index);
            }
        });
    });

    function addTimeSlot(content_elem, newTime, id) {
        var $newSlot = $('<div class="time-slot" data-id="' + id + '">' + newTime + '<button type="button"><i class="fa fa-times"></i></button></div>');
        $newSlot.find("button").on('click', (e) => {
            removeTimeSlot(e);
        }); 

        var inserted = false;

        content_elem.find('.time-slot').each(function () {
            var currentTime = $(this).text().trim();

            let newDate = new Date('1970/01/01 ' + newTime);
            let currDate = new Date('1970/01/01 ' + currentTime);

            if (newDate < currDate) {
                $newSlot.insertBefore($(this));
                inserted = true;
                return false; // break the loop
            }
        });

        if (!inserted) {
            content_elem.append($newSlot);
        }
    }

    // Usage: addTimeSlot('11:49 PM');

    function convertTo24Hour(time) {
        var hours = parseInt(time.match(/^(\d+)/)[1]);
        var minutes = parseInt(time.match(/:(\d+)/)[1]);
        var AMPM = time.match(/\s(AM|PM)$/i)[1];
        if (AMPM.toUpperCase() === 'PM' && hours < 12) hours += 12;
        if (AMPM.toUpperCase() === 'AM' && hours === 12) hours -= 12;
        var sHours = hours.toString();
        var sMinutes = minutes.toString();
        if (hours < 10) sHours = '0' + sHours;
        if (minutes < 10) sMinutes = '0' + sMinutes;
        return sHours + ':' + sMinutes;
    }

});
