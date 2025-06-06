// Colorblind Mode
document.addEventListener('DOMContentLoaded', function() {
    var switchControl = document.getElementById('switch1');
    // 加载已存储的设置
    switchControl.checked = localStorage.getItem('colorblind') === 'true';

    switchControl.addEventListener('change', function() {
        if (this.checked) {
            document.body.classList.add('colorblind');
            localStorage.setItem('colorblind', 'true'); // 存储设置状态
        } else {
            document.body.classList.remove('colorblind');
            localStorage.setItem('colorblind', 'false'); // 存储设置状态
        }
    });
});

// Bigger Size Mode
document.addEventListener('DOMContentLoaded', function() {
    var fontSizeSwitch = document.getElementById('switch2');
    fontSizeSwitch.checked = localStorage.getItem('fontSizeBigger') === 'true';

    fontSizeSwitch.addEventListener('change', function() {
        if (this.checked) {
            document.documentElement.style.fontSize = '127%';
            localStorage.setItem('fontSizeBigger', 'true');
        } else {
            document.documentElement.style.fontSize = '100%';
            localStorage.setItem('fontSizeBigger', 'false');
        }
    });
});

// Web Reader
document.addEventListener('DOMContentLoaded', function() {
    var narrationSwitch = document.getElementById('switch3');
    narrationSwitch.checked = localStorage.getItem('narration') === 'true';

    var synth = window.speechSynthesis;
    var utterance = new SpeechSynthesisUtterance(document.body.innerText);

    narrationSwitch.addEventListener('change', function() {
        if (this.checked) {
            if (synth.speaking) {
                console.error('Speech synthesis already speaking.');
                return;
            }
            synth.speak(utterance);
            localStorage.setItem('narration', 'true');
        } else {
            synth.cancel();
            localStorage.setItem('narration', 'false');
        }
    });

    utterance.onend = function() {
        narrationSwitch.checked = false;
        localStorage.setItem('narration', 'false');
    };
});


// Save the setting
document.addEventListener('DOMContentLoaded', function() {
    // 色盲模式
    if (localStorage.getItem('colorblind') === 'true') {
        document.body.classList.add('colorblind');
        document.getElementById('switch1').checked = true;
    }

    // 字体大小
    if (localStorage.getItem('fontSizeBigger') === 'true') {
        document.documentElement.style.fontSize = '140%';
        document.getElementById('switch2').checked = true;
    }

    // 自动朗读
    if (localStorage.getItem('narration') === 'true') {
        document.getElementById('switch3').checked = true;
        // 可以选择是否自动开始朗读
    }
});
