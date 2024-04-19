<script lang="ts">
import type { Competition } from "../types/Competition"
import Config from "../config";


export default {
  data():{
    loading: boolean;
    competitions: Competition[];
  } {
    return {
      loading: false,
      competitions: [],
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    fetchData() {
      this.loading = true;
      fetch(`${Config.API_URL}/competitions`)
        .then(response => response.json())
        .then(data => {
            this.competitions = data as Competition[];
            this.loading = false;
        });
    },
  },
}
</script>

<template>
    <main>
        <div v-if="loading">LOADING</div>
        <ul>
            <li v-for="competition in competitions" :key="competition.id">
                {{ competition.name }}
            </li>
        </ul>
    </main>
</template>

<style scoped>
main {
  display: flex;
  justify-content: center;
}
ul {
  font-size: 30px;
  list-style-type: none;
}
li {
  padding: 10px 0;
  letter-spacing: 1px;
}
</style>