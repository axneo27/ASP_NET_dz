import { useEffect, useState } from "react";
import { Box, Typography, List, ListItem, ListItemText, Paper } from "@mui/material";
import axios from "axios";
import { apiUrl } from "../../env";

interface Genre {
  id: string;
  name: string;
}

const GenrePage = () => {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get(`${apiUrl}/genre`)
      .then(res => setGenres(res.data.payload || []))
      .finally(() => setLoading(false));
  }, []);

  return (
    <Box maxWidth={600} mx="auto" my={4}>
      <Typography variant="h4" mb={2}>Жанри</Typography>
      {loading ? (
        <Typography>Завантаження...</Typography>
      ) : (
        <Paper>
          <List>
            {genres.map(g => (
              <ListItem key={g.id}>
                <ListItemText primary={g.name} />
              </ListItem>
            ))}
          </List>
        </Paper>
      )}
    </Box>
  );
};

export default GenrePage;
