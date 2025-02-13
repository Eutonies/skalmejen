export class SkalmejenSpotifyClient {
    static setup(apiToken) {

        const script = document.createElement("script")
        script.src = "https://sdk.scdn.co/spotify-player.js"
        script.async = true;
        document.body.appendChild(script)

        function printState(player) {
            player.getCurrentState().then(state => {
                if (!state) {
                    console.error('User is not playing music through the Web Playback SDK');
                    return;
                }

                var current_track = state.track_window.current_track;
                var next_track = state.track_window.next_tracks[0];

                console.log('Currently Playing', current_track);
                console.log('Playing Next', next_track);
            });
        }

        window.onSpotifyWebPlaybackSDKReady = () => {

            const player = new window.Spotify.Player({
                name: 'Web Playback SDK',
                getOAuthToken: cb => { cb(apiToken); },
                volume: 0.5
            });

            player.addListener('ready', ({ device_id }) => {
                console.log('Ready with Device ID', device_id)
                player.togglePlay()
                printState(player)

            });

            player.addListener('not_ready', ({ device_id }) => {
                console.log('Device ID has gone offline', device_id)
            });
            player.connect()

        };
    }
}

