/*! SmartAdmin - v1.4.1 - 2014-06-26 */
var root = this,
    SpeechRecognition = root.SpeechRecognition || root.webkitSpeechRecognition || root.mozSpeechRecognition || root.msSpeechRecognition || root.oSpeechRecognition;
if (SpeechRecognition && voice_command) {
    $.root_.on("click", '[data-action="voiceCommand"]', function (e) {
        $.root_.hasClass("voice-command-active") ? $.speechApp.stop() : ($.speechApp.start(), $("#speech-btn .popover").fadeIn(350)), e.preventDefault()
    }), $(document).mouseup(function (e) {
        $("#speech-btn .popover").is(e.target) || 0 !== $("#speech-btn .popover").has(e.target).length || $("#speech-btn .popover").fadeOut(250)
    });
    var modal = $('<div class="modal fade" id="voiceModal" tabindex="-1" role="dialog" aria-labelledby="remoteModalLabel" aria-hidden="true"><div class="modal-dialog"><div class="modal-content"></div></div></div>');
    modal.appendTo("body"), $.speechApp = function (e) {
        return e.start = function () {
            smartSpeechRecognition.addCommands(commands), smartSpeechRecognition ? (smartSpeechRecognition.start(), $.root_.addClass("voice-command-active"), $.speechApp.playON(), voice_localStorage && localStorage.setItem("sm-setautovoice", "true")) : alert("speech plugin not loaded")
        }, e.stop = function () {
            smartSpeechRecognition && (smartSpeechRecognition.abort(), $.root_.removeClass("voice-command-active"), $.speechApp.playOFF(), voice_localStorage && localStorage.setItem("sm-setautovoice", "false"), $("#speech-btn .popover").is(":visible") && $("#speech-btn .popover").fadeOut(250))
        }, e.playON = function () {
            var e = document.createElement("audio");
            navigator.userAgent.match("Firefox/") ? e.setAttribute("src", $.sound_path + "voice_on.ogg") : e.setAttribute("src", $.sound_path + "voice_on.mp3"), e.addEventListener("load", function () {
                e.play()
            }, !0), $.sound_on && (e.pause(), e.play())
        }, e.playOFF = function () {
            var e = document.createElement("audio");
            navigator.userAgent.match("Firefox/") ? e.setAttribute("src", $.sound_path + "voice_off.ogg") : e.setAttribute("src", $.sound_path + "voice_off.mp3"), $.get(), e.addEventListener("load", function () {
                e.play()
            }, !0), $.sound_on && (e.pause(), e.play())
        }, e.playConfirmation = function () {
            var e = document.createElement("audio");
            navigator.userAgent.match("Firefox/") ? e.setAttribute("src", $.sound_path + "voice_alert.ogg") : e.setAttribute("src", $.sound_path + "voice_alert.mp3"), $.get(), e.addEventListener("load", function () {
                e.play()
            }, !0), $.sound_on && (e.pause(), e.play())
        }, e
    }({})
} else $("#speech-btn").addClass("display-none");
(function (e) {
    "use strict";
    if (!SpeechRecognition) return root.smartSpeechRecognition = null, e;
    var o, t, n = [],
        a = {
            start: [],
            error: [],
            end: [],
            result: [],
            resultMatch: [],
            resultNoMatch: [],
            errorNetwork: [],
            errorPermissionBlocked: [],
            errorPermissionDenied: []
        }, r = 0,
        c = !1,
        i = "font-weight: bold; color: #00f;",
        s = /\s*\((.*?)\)\s*/g,
        l = /(\(\?:[^)]+\))\?/g,
        d = /(\(\?)?:\w+/g,
        p = /\*\w+/g,
        u = /[\-{}\[\]+?.,\\\^$|#]/g,
        m = function (e) {
            return e = e.replace(u, "\\$&").replace(s, "(?:$1)?").replace(d, function (e, o) {
                return o ? e : "([^\\s]+)"
            }).replace(p, "(.*?)").replace(l, "\\s*$1?\\s*"), new RegExp("^" + e + "$", "i")
        }, g = function (e) {
            e.forEach(function (e) {
                e.callback.apply(e.context)
            })
        }, h = function () {
            v() || root.smartSpeechRecognition.init({}, !1)
        }, v = function () {
            return o !== e
        };
    root.smartSpeechRecognition = {
        init: function (s, l) {
            l = l === e ? !0 : !!l, o && o.abort && o.abort(), o = new SpeechRecognition, o.maxAlternatives = 5, o.continuous = !0, o.lang = voice_command_lang || "en-US", o.onstart = function () {
                g(a.start), $.root_.removeClass("service-not-allowed"), $.root_.addClass("service-allowed")
            }, o.onerror = function (e) {
                switch (g(a.error), e.error) {
                    case "network":
                        g(a.errorNetwork);
                        break;
                    case "not-allowed":
                    case "service-not-allowed":
                        t = !1, $.root_.removeClass("service-allowed"), $.root_.addClass("service-not-allowed"), g((new Date).getTime() - r < 200 ? a.errorPermissionBlocked : a.errorPermissionDenied)
                }
            }, o.onend = function () {
                if (g(a.end), t) {
                    var e = (new Date).getTime() - r;
                    1e3 > e ? setTimeout(root.smartSpeechRecognition.start, 1e3 - e) : root.smartSpeechRecognition.start()
                }
            }, o.onresult = function (e) {
                g(a.result);
                for (var o, t = e.results[e.resultIndex], r = 0; r < t.length; r++) {
                    o = t[r].transcript.trim(), c && root.console.log("Speech recognized: %c" + o, i);
                    for (var s = 0, l = n.length; l > s; s++) {
                        var d = n[s].command.exec(o);
                        if (d) {
                            var p = d.slice(1);
                            c && (root.console.log("command matched: %c" + n[s].originalPhrase, i), p.length && root.console.log("with parameters", p)), n[s].callback.apply(this, p), g(a.resultMatch);
                            var u = ["sound on", "mute", "stop"];
                            return u.indexOf(n[s].originalPhrase) < 0 && ($.smallBox({
                                title: n[s].originalPhrase,
                                content: "loading...",
                                color: "#333",
                                sound_file: "voice_alert",
                                timeout: 2e3
                            }), $("#speech-btn .popover").is(":visible") && $("#speech-btn .popover").fadeOut(250)), !0
                        }
                    }
                }
                return g(a.resultNoMatch), $.smallBox({
                    title: 'Error: <strong> " ' + o + ' " </strong> no match found!',
                    content: "Please speak clearly into the microphone",
                    color: "#a90329",
                    timeout: 5e3,
                    icon: "fa fa-microphone"
                }), $("#speech-btn .popover").is(":visible") && $("#speech-btn .popover").fadeOut(250), !1
            }, l && (n = []), s.length && this.addCommands(s)
        },
        start: function (n) {
            h(), n = n || {}, t = n.autoRestart !== e ? !!n.autoRestart : !0, r = (new Date).getTime(), o.start()
        },
        abort: function () {
            t = !1, v && o.abort()
        },
        debug: function (e) {
            c = arguments.length > 0 ? !!e : !0
        },
        setLanguage: function (e) {
            h(), o.lang = e
        },
        addCommands: function (e) {
            var o, t;
            h();
            for (var a in e)
                if (e.hasOwnProperty(a)) {
                    if (o = root[e[a]] || e[a], "function" != typeof o) continue;
                    t = m(a), n.push({
                        command: t,
                        callback: o,
                        originalPhrase: a
                    })
                }
            c && root.console.log("Commands successfully loaded: %c" + n.length, i)
        },
        removeCommands: function (o) {
            return o === e ? void (n = []) : (o = Array.isArray(o) ? o : [o], void (n = n.filter(function (e) {
                for (var t = 0; t < o.length; t++)
                    if (o[t] === e.originalPhrase) return !1;
                return !0
            })))
        },
        addCallback: function (o, t, n) {
            if (a[o] !== e) {
                var r = root[t] || t;
                "function" == typeof r && a[o].push({
                    callback: r,
                    context: n || this
                })
            }
        }
    }
}).call(this);
var autoStart = function () {
    smartSpeechRecognition.addCommands(commands), smartSpeechRecognition ? (smartSpeechRecognition.start(), $.root_.addClass("voice-command-active"), voice_localStorage && localStorage.setItem("sm-setautovoice", "true")) : alert("speech plugin not loaded")
};
SpeechRecognition && voice_command && "true" == localStorage.getItem("sm-setautovoice") && autoStart(), SpeechRecognition && voice_command_auto && voice_command && autoStart();
