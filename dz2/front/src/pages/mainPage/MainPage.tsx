import LinearProgress from "@mui/material/LinearProgress";
import { useGetTracksQuery } from "../../store/services/trackApi";
import type { Track } from "./types";
import TrackCard from "../../components/cards/TrackCard";
import AudioPlayer from "react-h5-audio-player";
import "react-h5-audio-player/lib/styles.css";
import { audioUrl } from "../../env";
import { Box, Typography } from "@mui/material";
import "./mainPage.css";
import { useState } from "react";

const MainPage = () => {
    const [index, setIndex] = useState<number>(0);
    const { data, isLoading } = useGetTracksQuery(null);

    const nextHandler = () => {
        const count = data?.payload.length;
        if(index < count - 1) {
            setIndex(prev => prev + 1);
        } else {
            setIndex(0);
        }
    };

    const prevHandler = () => {
        const count = data?.payload.length;
        if(index > 0) {
            setIndex(prev => prev - 1);
        } else {
            setIndex(count - 1);
        }
    };

    return (
        <>
            {isLoading ? (
                <LinearProgress color="secondary" />
            ) : (
                <Box minHeight="100vh">
                    {data?.payload.map((track: Track, index: number) => (
                        <TrackCard key={track.id} track={track} index={index} setIndex={setIndex} />
                    ))}
                    <Box className="playerBox">
                        <Typography sx={{mx: 2}}>{data?.payload[index].title}</Typography>
                        <AudioPlayer
                            autoPlayAfterSrcChange
                            onClickPrevious={prevHandler}
                            onClickNext={nextHandler}
                            showSkipControls
                            src={`${audioUrl}/${data?.payload[index].audioUrl}`}
                        />
                    </Box>
                </Box>
            )}
        </>
    );
};

export default MainPage;
