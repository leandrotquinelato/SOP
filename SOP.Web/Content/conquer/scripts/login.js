var Login = function () {

	var handleLogin = function() {

	    $('.login-form input').keypress(function (e) {
	        if (e.which == 13) {
                $('.login-form').submit();
	            return false;
	        }
	    });
	}
    
    return {

        init: function () {
        	
            handleLogin();

			$.backstretch([
		        "/Content/conquer/img/bg/1.jpg",
		        "/Content/conquer/img/bg/2.jpg",
		        "/Content/conquer/img/bg/3.jpg"
		        ], {
		          fade: 1000,
		          duration: 5000
		    });
			
	       
        }

    };

}();