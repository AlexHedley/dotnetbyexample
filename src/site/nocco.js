// How to Add Copy to Clipboard Buttons to Code Blocks in Hugo - March 22, 2019  
// https://www.dannyguo.com/blog/how-to-add-copy-to-clipboard-buttons-to-code-blocks-in-hugo/

// $("#copyscriptbutton").on("click", function() {
// var element = document.getElementById("copyscriptbutton");
// element.addEventListener('click', function() {
const btns = document.querySelectorAll('button[id^=copyscriptbutton]');
btns.forEach(btn => {

    btn.addEventListener('click', event => {

        // var button = $(this)[0];
        // console.log(button);
        // button.innerText = 'Copied!';
        // element.textContent = 'Copied!';
        btn.textContent = 'Copied!';
        // var text = $('pre > code')[0].innerText;
        // var text = document.querySelector('.code > pre > code').innerText;
        //var text = btn.closest('pre > code'); //.innerText;
        var text = btn.nextElementSibling.innerText;
        console.log(text);
        //document.execCommand("copy");
        // clipboard.writeText(text);
        navigator.clipboard.writeText(text);

        setTimeout(function () {
            // button.innerText = 'Copy';
            // element.innerText = 'Copy';
            btn.textContent = 'Copied!';
        }, 2000);

    });

});
// }, false);
// });