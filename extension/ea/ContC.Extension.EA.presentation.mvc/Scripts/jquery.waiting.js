(function($) {
	$.fn.closing = function(options) {
		options = $.extend({modo: 'normal'}, options);
		
		this.fadeOut(options.modo);
		
		return this;
	};
})(jQuery);

(function ($) {
    $.fn.showing = function (options) {
        options = $.extend({ modo: 'normal' }, options);

        this.fadeIn(options.modo);

        return this;
    };
})(jQuery);