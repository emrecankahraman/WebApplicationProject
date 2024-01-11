document.addEventListener("DOMContentLoaded", function () {
    // Netflix grafiği
    $.ajax({
        url: '/StatisticDetails/GetChartData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var ctxNetflix = document.getElementById('myChartNetflix').getContext('2d');
            var myChartNetflix = new Chart(ctxNetflix, {
                type: 'bar',
                data: {
                    labels: ['NetflixSentMail', 'NetflixSuccess', 'NetflixNotSuccess'],
                    datasets: [{
                        label: 'Email Phishing',
                        data: [data.netflixSentMail, data.netflixSuccessCount, data.netflixNotSuccessCount],
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },
        error: function (error) {
            console.log(error);
        }
    });

    // Spotify grafiği
    $.ajax({
        url: '/StatisticDetails/GetChartData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var ctxSpotify = document.getElementById('myChartSpotify').getContext('2d');
            var myChartSpotify = new Chart(ctxSpotify, {
                type: 'bar',
                data: {
                    labels: ['SpotifySentMail', 'SpotifySuccess', 'SpotifyNotSuccess'],
                    datasets: [{
                        label: 'Email Phishing',
                        data: [data.spotifySentMail, data.spotifySuccessCount, data.spotifyNotSuccessCount],
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },
        error: function (error) {
            console.log(error);
        }
    });

    // Facebook grafiği
    $.ajax({
        url: '/StatisticDetails/GetChartData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var ctxFacebook = document.getElementById('myChartFacebook').getContext('2d');
            var myChartFacebook = new Chart(ctxFacebook, {
                type: 'bar',
                data: {
                    labels: ['FacebookSentMail', 'FacebookSuccess', 'FacebookNotSuccess'],
                    datasets: [{
                        label: 'Email Phishing',
                        data: [data.facebookSentMail, data.facebookSuccessCount, data.facebookNotSuccessCount],
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },
        error: function (error) {
            console.log(error);
        }
    });
});
