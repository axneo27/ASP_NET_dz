import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { apiUrl } from '../../env';
import type { ServiceResponse } from './types';

export const trackApi = createApi({
    baseQuery: fetchBaseQuery({
        baseUrl: apiUrl
    }),
    tagTypes: ['tracks'],
    reducerPath: 'track',
    endpoints: (build) => ({
        getTracks: build.query<ServiceResponse, null>({
            query: () => ({
                url: 'track',
                method: 'get'
            }),
            providesTags: ['tracks']
        })
    })
});

export const { useGetTracksQuery } = trackApi;