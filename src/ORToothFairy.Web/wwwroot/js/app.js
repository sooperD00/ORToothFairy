window.browserInterop = {
    getCurrentPosition: function () {
        return new Promise((resolve, reject) => {
            if (!navigator.geolocation) {
                resolve(null);
                return;
            }
            
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    resolve({
                        latitude: position.coords.latitude,
                        longitude: position.coords.longitude
                    });
                },
                (error) => {
                    console.log('Geolocation error:', error.message);
                    resolve(null);
                },
                {
                    enableHighAccuracy: false,
                    timeout: 10000,
                    maximumAge: 300000
                }
            );
        });
    },

    getItem: function (key) {
        return localStorage.getItem(key);
    },

    setItem: function (key, value) {
        localStorage.setItem(key, value);
    },

    removeItem: function (key) {
        localStorage.removeItem(key);
    }
};
