import { Box, Button, FormControl, FormLabel, MenuItem, Select, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import axios from "axios";
import { apiUrl } from "../../env";

interface Genre {
  id: string;
  name: string;
}

const TrackCreatePage = () => {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [form, setForm] = useState({
    title: "",
    description: "",
    posterUrl: "",
    releaseDate: "",
    genreId: ""
  });
  const [audioFile, setAudioFile] = useState<File | null>(null);
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    axios.get(`${apiUrl}/genre`).then(res => setGenres(res.data.payload || []));
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setForm(f => ({ ...f, [e.target.name]: e.target.value }));
  };

  const handleGenreChange = (e: any) => {
    setForm(f => ({ ...f, genreId: e.target.value }));
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) setAudioFile(e.target.files[0]);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!audioFile) return;
    setLoading(true);
    const data = new FormData();
    data.append("title", form.title);
    data.append("description", form.description);
    data.append("posterUrl", form.posterUrl);
    data.append("releaseDate", form.releaseDate);
    data.append("genreId", form.genreId);
    data.append("audioFile", audioFile);
    try {
      await axios.post(`${apiUrl}/track`, data, { headers: { "Content-Type": "multipart/form-data" } });
      setSuccess(true);
      setForm({ title: "", description: "", posterUrl: "", releaseDate: "", genreId: "" });
      setAudioFile(null);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box maxWidth={600} mx="auto" my={4}>
      <Typography variant="h4" mb={2}>Додати трек</Typography>
      <form onSubmit={handleSubmit}>
        <FormControl fullWidth margin="dense">
          <FormLabel>Назва</FormLabel>
          <TextField name="title" value={form.title} onChange={handleChange} required />
        </FormControl>
        <FormControl fullWidth margin="dense">
          <FormLabel>Опис</FormLabel>
          <TextField name="description" value={form.description} onChange={handleChange} />
        </FormControl>
        <FormControl fullWidth margin="dense">
          <FormLabel>Постер (URL)</FormLabel>
          <TextField name="posterUrl" value={form.posterUrl} onChange={handleChange} />
        </FormControl>
        <FormControl fullWidth margin="dense">
          <FormLabel>Дата релізу</FormLabel>
          <TextField name="releaseDate" type="date" value={form.releaseDate} onChange={handleChange} InputLabelProps={{ shrink: true }} />
        </FormControl>
        <FormControl fullWidth margin="dense">
          <FormLabel>Жанр</FormLabel>
          <Select name="genreId" value={form.genreId} onChange={handleGenreChange} required>
            {genres.map(g => <MenuItem key={g.id} value={g.id}>{g.name}</MenuItem>)}
          </Select>
        </FormControl>
        <FormControl fullWidth margin="dense">
          <FormLabel>Аудіофайл</FormLabel>
          <input type="file" accept="audio/*" onChange={handleFileChange} required />
        </FormControl>
        <Button type="submit" variant="contained" sx={{ mt: 2 }} disabled={loading}>Додати</Button>
        {success && <Typography color="success.main" mt={2}>Трек додано!</Typography>}
      </form>
    </Box>
  );
};

export default TrackCreatePage;
